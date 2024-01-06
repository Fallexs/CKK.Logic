using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Linq.Expressions;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer Customer { get; set; }
        private List<ShoppingCartItem> Products { get; set; } = new();
        public List<ShoppingCartItem> GetProducts() => Products;
        public int? GetCustomerId() => Customer.Id;
        public ShoppingCart(Customer cust) {
            Customer = cust;
        }
        public ShoppingCartItem? GetProductById(int id) {
            try {
                if (id < 0) {
                    throw new InvalidIdException();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
            var Product =
                from e in Products
                where id == e.Product?.Id
                select e;
            return Product as ShoppingCartItem;
        }

        public ShoppingCartItem? AddProduct(Product prod, int quant) {
            var existing =
                from e in Products
                where prod == e.Product
                select e;
            try {
                if( quant < 0 ) {
                    throw new InventoryItemStockTooLowException();
                }
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
            if( existing.Any() ) {
                foreach( ShoppingCartItem item in existing ) {
                    item.Quantity += quant;
                    return item;
                }
            }
            var newProduct = new ShoppingCartItem(prod, quant);
            Products.Add(newProduct);
            return newProduct;

        }

        public ShoppingCartItem? RemoveProduct(int id, int quant) {
            var Existing =
                from e in Products
                where id == e.Product?.Id
                select e;
            try {
                if( !Existing.Any() ) {
                    throw new ProductDoesNotExistException();
                }
                if( quant < 0 ) {
                    throw new InventoryItemStockTooLowException();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
            foreach(var item in Existing) {
                if( item.Quantity > 0 ) {
                    item.Quantity -= quant;
                    if( item.Quantity < 0 ) {
                        Products.Remove(item);
                    }
                    return item;
                }
            }
            return null;
        }

        public decimal? GetTotal() {
            var GetTotal =
                from e in Products
                let TotalPrice = e.Product?.Price * e.Quantity
                select TotalPrice;
            foreach(var item in GetTotal) {
                return item;
            }
            return null;
        }
    }
}
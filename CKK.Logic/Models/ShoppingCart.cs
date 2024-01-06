using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Linq.Expressions;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer Customer { get; set; }
        public List<ShoppingCartItem> Products = new();
        public List<ShoppingCartItem> GetProducts() => Products;
        public int GetCustomerId() => Customer.Id;
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
                where e.Product.Id.Equals(id)
                select e;
            return Product as ShoppingCartItem;
        }

        public ShoppingCartItem? AddProduct(Product prod, int quant) {
            var existing =
                from e in Products
                where prod == e.Product
                select e;
            if (prod != null && quant > 0) {
                if(existing.Any()) {
                    foreach(ShoppingCartItem item in existing) {
                        item.Quantity += quant;
                        return item;
                    }
                } else {
                    var newProduct = new ShoppingCartItem(prod, quant);
                    Products.Add(newProduct);
                    return newProduct;
                }
            } else if (quant <= 0) {
                throw new InventoryItemStockTooLowException();
            } return null;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quant) {
            try {
                var Existing =
                    from e in Products
                    where id.Equals(e.Product.Id)
                    select e;
                var Product = Existing.ToList().DefaultIfEmpty(null).First();
                if( Product == null ) {
                    throw new ProductDoesNotExistException();
                } else if( quant < 0 ) {
                    throw new InventoryItemStockTooLowException();
                } else {
                    return Product;
                }
            }
            catch (ProductDoesNotExistException ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (InventoryItemStockTooLowException ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public decimal GetTotal() {
            var GetTotal =
                from e in Products
                let TotalPrice = e.Product.Price * e.Quantity
                select TotalPrice;
            var Total = GetTotal.FirstOrDefault();
            return Total;
        }
    }
}
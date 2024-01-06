using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Linq.Expressions;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer Customer { get; set; }
        private List<ShoppingCartItem> Products = new();
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

        public ShoppingCartItem AddProduct(Product prod, int quant) {
            var existing =
                from e in Products
                where prod.Equals(e.Product)
                select e;
            var Product = existing.FirstOrDefault();
            if (Product == null) {
                Product = new ShoppingCartItem(prod, quant);
                Products.Add(Product);
            } else {
                Product.Quantity += quant;
            }
            return Product;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quant) {
            var Existing =
                from e in Products
                where id.Equals(e.Product.Id)
                select e;
            var Product = Existing.First();
            try {
                if( Product == null ) {
                    throw new ProductDoesNotExistException();
                } else if( id <= 0 ) {
                    throw new InvalidIdException();
                } else if( quant < 0 ) {
                    throw new InventoryItemStockTooLowException();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
            int _ = Product.Quantity - quant;
            if( _ < 0 ) {
                Product.Quantity = 0;
                Products.Remove(Product);
            } else if ( _ > 0 ) {
                Product.Quantity = _;
            }
            return Product;
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
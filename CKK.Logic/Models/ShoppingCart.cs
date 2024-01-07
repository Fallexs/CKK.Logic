using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        public Customer Customer { get; set; }
        public List<ShoppingCartItem> Products { get; set; }
        public List<ShoppingCartItem> GetProducts() => Products;
        public int GetCustomerId() => Customer.Id;
        public ShoppingCart(Customer cust) {
            Customer = cust;
            Products = new List<ShoppingCartItem>();
        }

        public ShoppingCartItem AddProduct(Product prod, int quant) {
            if ( quant <= 0 ) {
                throw new InventoryItemStockTooLowException();
            }
            var existing = Products.SingleOrDefault(item => prod == item.Product);
            if ( existing != null ) {
                existing.Quantity += quant;
                return existing;
            }

            Products.Add(new ShoppingCartItem(prod, quant));
            return Products.Last();
        }

        public ShoppingCartItem RemoveProduct(int id, int quant) {

            var existing = Products.SingleOrDefault(product => id == product.Product.Id);
                if( quant < 0 ) {
                    throw new ArgumentOutOfRangeException(nameof(quant), "Invalid Quantity.");
                }

                if( existing is null ) {

                    throw new ProductDoesNotExistException();
                }
            existing.Quantity -= quant;

            if(existing.Quantity - quant <= 0 ) {
                Products.Remove(existing);
                return new ShoppingCartItem(null, 0);
            }
            return existing;
        }

        public ShoppingCartItem GetProductById(int id) {
                if ( id < 0 ) {
                throw new InvalidIdException();
                } 

            var existing = Products.SingleOrDefault(product => id == product.Product.Id);

            if (existing is null) {
                return null;
            }
            return existing;
        }

        public decimal GetTotal() {
            var GetTotal =
                from e in Products
                let TotalPrice = e.Product.Price * e.Quantity
                select TotalPrice;
            decimal Total = 0m;
            foreach(var item in GetTotal) {
                Total += item;
            }
            return Total;
        }
    }
}
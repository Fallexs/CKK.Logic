using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Reflection.Metadata.Ecma335;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart {


        public Customer Customer { get; set; }
        public List<ShoppingCartItem> Products { get; set; }

        public ShoppingCart(Customer cust) {
            Customer = cust;
            Products = new List<ShoppingCartItem>();
        }
        public int GetCustomerId() {
            return Customer.Id;
        }
        public ShoppingCartItem GetProductById(int id) {
            if( id < 0 ) {
                throw new InvalidIdException();
            }
            var Existing = Products.Where(prod => id == prod.Product.Id).ToList();
            if ( Existing.Count == 0 ) {
                return null;
            }
            return Existing.Single();
        }
        public ShoppingCartItem AddProduct(Product prod, int quantity) {
            if( quantity <= 0 ) {
                throw new InventoryItemStockTooLowException();
            }
            var Existing = Products.Where(item => prod == item.Product).ToList();
            if( Existing.Count == 0 ) {
                var addNew = new ShoppingCartItem(prod, quantity);
                Products.Add(addNew);
                return addNew;
            }
            if (Existing.Any()) {
                Existing.Single().Quantity += quantity;
            }
            return Existing.Single();
        }
        public ShoppingCartItem RemoveProduct(int id, int quantity) {
            if( quantity < 0 ) {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }
            var Existing = Products.Where(prod => id == prod.Product.Id).ToList();
            if ( Existing.Count == 0 ) {
                throw new ProductDoesNotExistException();
            }
                if( Existing.Single().Quantity - quantity <= 0 ) {
                    Products.Remove(Existing.Single());
                    return new ShoppingCartItem(null, 0);
                }
                Existing.First().Quantity = (Existing.First().Quantity - quantity);
                return Existing.First();
        }

        public decimal GetTotal() {
            decimal total = Products.Sum(x => x.GetTotal());
            return total;
        }
        public List<ShoppingCartItem> GetProducts() {
            return Products;
        }

    }
}
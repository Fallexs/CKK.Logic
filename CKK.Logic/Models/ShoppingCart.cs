using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


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
            List<int> Existing = Products.Select(x => x.Product.Id).ToList();
            for( int i = 0; i < Existing.Count; i++ ) {

                if( Existing [ i ] == id ) {
                    return Products [ i ];
                }
            }
            return null;
        }
        public ShoppingCartItem AddProduct(Product prod, int quantity) {
            if( quantity <= 0 ) {
                throw new InventoryItemStockTooLowException();
            }
            if( Products.Count == 0 ) {
                Products.Add(new ShoppingCartItem(prod, quantity));
                return Products [ 0 ];
            }
            for( int i = 0; i < Products.Count; i++ ) {
                if( Products [ i ].Product.Id == prod.Id ) {
                    Products [ i ].Quantity = (Products [ i ].Quantity + quantity);
                    return Products [ i ];
                }

            }
            Products.Add(new ShoppingCartItem(prod, quantity));
            return Products.Last();
        }
        public ShoppingCartItem RemoveProduct(int id, int quantity) {
            if( quantity < 0 ) {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }
            var Existing = Products.Where(prod => id == prod.Product.Id).ToList();
                if( Existing.Single().Quantity - quantity <= 0 ) {
                    Products.Remove(Existing.Single());
                    return new ShoppingCartItem(null, 0);
                }
            if( Existing.First() == null) {
                throw new ProductDoesNotExistException();
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
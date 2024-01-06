using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer Customer { get; set; }
        private List<ShoppingCartItem> Products { get; set; } = new();
        public List<ShoppingCartItem> GetProducts() => Products;
        public int GetCustomerId() => Customer.Id;
        public ShoppingCart(Customer cust) {
            Customer = cust;
        }
        public ShoppingCartItem GetProductById(int id) {
            var Existing =
                from e in Products
                where id.Equals(e.Product.Id)
                select e;
            var Product = Existing.FirstOrDefault();
            if (id < 0) {
                throw new InvalidIdException();
            }
            if (Product == null) {
                throw new ProductDoesNotExistException();
            }
            return Product;
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

        public ShoppingCartItem RemoveProduct(int id, int quant) {
            var Existing =
                from e in Products
                where id.Equals(e.Product.Id)
                select e;
            var Product = Existing.First();
            if( Product == null ) {
                throw new ProductDoesNotExistException();
            } else if( id <= 0 ) {
                throw new InvalidIdException();
            } else if( quant < 0 ) {
                throw new InventoryItemStockTooLowException();
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
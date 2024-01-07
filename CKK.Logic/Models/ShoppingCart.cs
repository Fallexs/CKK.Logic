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
        public int GetCustomerId() => Customer.Id;
        public ShoppingCart(Customer cust) {
            Customer = cust;
        }

        public ShoppingCartItem AddProduct(Product prod, int quant) {
            if ( quant <= 0 ) {
                throw new InventoryItemStockTooLowException();
            } else {
                var Existing = (
                    from product in Products
                    where prod == product.Product
                    select product);
                if ( Existing.Any() ) {
                    foreach(var product in Products ) {
                        product.Quantity += quant;
                        return product;
                    }
                } else {
                    var newProduct = new ShoppingCartItem(prod, quant);
                    Products.Add(newProduct);
                    return newProduct;
                }
                return Existing.Single();
            }
        }

        public ShoppingCartItem RemoveProduct(int id, int quant) {
            var Existing = (
                from product in Products
                where id == product.Product.Id
                select product);
            if ( quant < 0 ) {
                throw new ArgumentOutOfRangeException(nameof(quant), "Invalid Quantity.");
            } else if ( !Existing.Any() ) {
                throw new ProductDoesNotExistException();
            } else {
                foreach(ShoppingCartItem product in Existing) {
                    product.Quantity -= quant;
                    if (product.Quantity > 0) {
                        return product;
                    } else {
                        Products.Remove(product);
                        return new ShoppingCartItem();
                    }
                }
                return Existing.Single();
            }
        }
        public ShoppingCartItem? GetProductById(int id) {
            if ( id < 0 ) {
                throw new InvalidIdException();
            }

            return Products.FirstOrDefault(product => id == product.Product.Id);
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
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer Customer { get; set; }
        private List<ShoppingCartItem> Products { get; set; } = new();

        public ShoppingCart(Customer cust) {
            Customer = cust;
        }

        public int GetCustomerId() => Customer.Id;

        public ShoppingCartItem? GetProductById(int id) {
            var FindExisting =
                from e in Products
                where id == e.Product.Id
                select e;
            try {
                if( FindExisting.Any() ) {
                    foreach(ShoppingCartItem item in FindExisting) {
                        return item;
                    }
                }
                if(id < 0) {
                    throw new InvalidIdException();
                }
                if(!FindExisting.Any()) {
                    throw new ProductDoesNotExistException();
                }
                return null;
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ShoppingCartItem? AddProduct(Product prod, int quantity) {
            var existing =
                from e in GetProducts()
                where prod == e.Product
                select e;
            try {
                if( quantity > 0 ) {
                    if( existing.Any() ) {
                        foreach (var item in existing) {
                            item.Quantity += quantity;
                            return item;
                        }
                    } else {
                        var newItem = new ShoppingCartItem(prod, quantity);
                        Products.Add(newItem);
                        return newItem;
                    }
                } else throw new InventoryItemStockTooLowException();
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quantity) {
            var FindExisting =
                from e in Products
                where id == e.Product.Id
                select e;
            try {
                if ( quantity < 0 ) {
                    throw new ArgumentOutOfRangeException(nameof(quantity));
                } else {
                    if( FindExisting.Any() ) {
                        foreach( var item in FindExisting ) {
                            if( item.Quantity - quantity < 0 ) {
                                item.Quantity -= quantity;
                                Products.Remove(item);
                                return item;
                            } else item.Quantity -= quantity; return item;
                        }
                    } else throw new ProductDoesNotExistException();
                }
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public decimal? GetTotal()
        {
            var GetTotal =
                from e in Products
                let TotalPrice = e.Product.Price * e.Quantity
                select TotalPrice;
            if (GetTotal.Any())
            {
                decimal starting = 0m;
                foreach (var item in GetTotal)
                {
                    starting += item;
                }
                return starting;
            }
            return null;
        }

        public List<ShoppingCartItem> GetProducts() => Products;
    }
}
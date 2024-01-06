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

        public ShoppingCartItem GetProductById(int id) {
            var FindExisting =
                from e in Products
                where id == e.Product.Id
                select e;
            try {
                if(id < 0) {
                    throw new InvalidIdException();
                }
                if(!FindExisting.Any()) {
                    throw new ProductDoesNotExistException();
                }
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
            }
            if( FindExisting.Any() ) {
                foreach(ShoppingCartItem item in FindExisting) {
                    return item;
                }
            }
            throw new ProductDoesNotExistException();
        }

        public ShoppingCartItem? AddProduct(Product prod, int quantity) {
            try {
                if( quantity <= 0 ) {
                    throw new InventoryItemStockTooLowException();
                }

                var FindExisting =
                    from e in Products
                    where prod == e.Product
                    select e;
                if( FindExisting.Any() ) {
                    foreach( ShoppingCartItem item in FindExisting ) {
                        item.Quantity += quantity;
                        return item;
                    }
                }
                ShoppingCartItem newProduct = new(prod, quantity);
                Products.Add(newProduct);
                return newProduct;
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ShoppingCartItem? RemoveProduct(int id, int quantity) {
            try {
                if( quantity < 0 ) {
                    throw new ArgumentOutOfRangeException(nameof(quantity));
                }
                var FindExisting =
                    from e in Products
                    where id == e.Product.Id
                    select e;
                if(FindExisting.Any()) {
                    foreach( ShoppingCartItem item in FindExisting ) {
                        item.Quantity -= quantity;
                        if( item.Quantity < 0 ) {
                            Products.Remove(item);
                            return item;
                        }
                        return item;
                    }
                }
                throw new ProductDoesNotExistException();
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
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
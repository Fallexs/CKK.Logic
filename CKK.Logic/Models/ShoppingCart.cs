namespace CKK.Logic.Models {
    public class ShoppingCart {
        private readonly Customer _Customer;
        private readonly List<ShoppingCartItem> _Products = new();
        public List<ShoppingCartItem> GetProducts() => _Products;
        public int Get_CustomerId() => _Customer.GetId();

        public ShoppingCart(Customer cust) {
            _Customer = cust;
        }

        public ShoppingCartItem? GetProductById(int id) {
            var GetByID =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if( GetByID.Any() ) {
                foreach( var item in GetByID ) {
                    return item;
                }
            }
            return null;
        }

        public ShoppingCartItem? AddProduct(Product prod, int quantity) {
            var CheckForExisting =
                from e in _Products
                where prod == e.GetProduct()
                select e;
            if( !CheckForExisting.Any() ) {
                ShoppingCartItem newItem = new(prod, quantity);
                _Products.Add(newItem);
                return newItem;
            } else 
            foreach( var item in CheckForExisting ) {
                item.SetQuantity(quantity += item.GetQuantity());
                return item;
            }
            return null;
        }

        public ShoppingCartItem? RemoveProduct(Product prod, int quantity) {
            var CheckForExisting =
                from e in _Products
                where prod == e.GetProduct()
                select e;
            if( CheckForExisting.Any() ) {
                foreach( var item in CheckForExisting ) {
                    item.SetQuantity(item.GetQuantity() - quantity);
                    if( item.GetQuantity() < 0 ) {
                        _Products.Remove(item);
                        return item;
                    }
                }
            }
            return null;
        }

        public decimal? GetTotal() {
            var GetTotal =
                from e in _Products
                let TotalPrice = e.GetProduct().GetPrice() * e.GetQuantity()
                select TotalPrice;
            if( GetTotal.Any() ) {
                foreach( var item in GetTotal ) {
                    return item;
                }
            }
            return null;
        }
    }
}
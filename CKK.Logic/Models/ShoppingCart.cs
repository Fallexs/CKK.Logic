namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer _customer { get; set; }
        private List<ShoppingCartItem> _Products { get; set; } = new();
        public List<ShoppingCartItem> GetProducts() => _Products;
        public int Get_CustomerId() => _customer.GetId();

        public ShoppingCart(Customer cust)
        {
            _customer = cust;
        }

<<<<<<< HEAD
        public ShoppingCartItem? GetProductById(int id)
        {
=======
        public ShoppingCartItem? AddProduct(Product prod) {
            return AddProduct(prod, 1);
        }

        public ShoppingCartItem? GetProductById(int id) {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            var GetByID =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if (GetByID.Any())
            {
                foreach (var item in GetByID)
                {
                    return item;
                }
            }
            return null;
        }

<<<<<<< HEAD
        public ShoppingCartItem? AddProduct(Product prod, int quantity)
        {
=======
        public ShoppingCartItem? AddProduct(Product prod, int quantity) {
            ShoppingCartItem newItem = new(prod, quantity);
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            var CheckForExisting =
                from e in _Products
                where prod == e.GetProduct()
                select e;
<<<<<<< HEAD
            if (!CheckForExisting.Any())
            {
                ShoppingCartItem newItem = new(prod, quantity);
                _Products.Add(newItem);
                return newItem;
            }
            else
                foreach (var item in CheckForExisting)
                {
                    item.SetQuantity(item.GetQuantity() + quantity);
                }
            return null;
        }

        public ShoppingCartItem? AddProduct(Product prod)
        {
            return AddProduct(prod, 1);
        }

        public ShoppingCartItem? RemoveProduct(Product prod, int quantity)
        {
=======
            if( quantity <= 0m ) {
                return null;
            }
            if( CheckForExisting.Any() ) {
                foreach( var item in CheckForExisting ) {
                    item.SetQuantity(quantity + item.GetQuantity());
                    return item;
                }
            } else _Products.Add(newItem);
            return newItem;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quantity) {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            var CheckForExisting =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if (CheckForExisting.Any())
            {
                foreach (var item in CheckForExisting)
                {
                    item.SetQuantity(item.GetQuantity() - quantity);
<<<<<<< HEAD
                    if (item.GetQuantity() < 0)
                    {
=======
                    if( item.GetQuantity() <= 0m ) {
                        item.SetQuantity(0);
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
                        _Products.Remove(item);
                    }
                    return item;
                }
            }
            return null;
        }

        public decimal? GetTotal()
        {
            var GetTotal =
                from e in _Products
                let TotalPrice = e.GetProduct().GetPrice() * e.GetQuantity()
                select TotalPrice;
<<<<<<< HEAD
            if (GetTotal.Any())
            {
                decimal starting = 0;
                foreach (var item in GetTotal)
                {
=======
            if( GetTotal.Any() ) {
                decimal starting = 0m;
                foreach( var item in GetTotal ) {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
                    starting += item;
                }
                return starting;
            }
            return null;
        }
    }
}
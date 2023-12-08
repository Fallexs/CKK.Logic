using System.ComponentModel;
using System.Reflection;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer _customer { get; set; }
        private List<ShoppingCartItem> _Products { get; set; } = new();
        public List<ShoppingCartItem> GetProducts() => _Products;
        public int Get_CustomerId() => _customer.GetId();

        public ShoppingCart(Customer cust) {
            _customer = cust;
        }

        public ShoppingCartItem? GetProductById(int id)
        {
            var GetByID =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if (GetByID.Any())
            {
                foreach (ShoppingCartItem item in GetByID)
                {
                    return item;
                }
            }
            return null;
        }

        public ShoppingCartItem? AddProduct(Product prod, int quantity)
        {
            var GetExisting =
                    from e in _Products
                    where prod == e.GetProduct()
                    select e;
            if (prod != null && quantity > 0)
            {
                if (GetExisting.Any())
                {
                    foreach (ShoppingCartItem item in GetExisting)
                    {
                        item.SetQuantity(item.GetQuantity() + quantity);
                        return item;
                    }
                }
                else
                {
                    var newItem = new ShoppingCartItem(prod, quantity);
                    _Products.Add(newItem);
                    return newItem;
                }
            }
            return null;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quantity)
        {
            var CheckForExisting =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if (CheckForExisting.Any())
            {
                foreach (ShoppingCartItem item in CheckForExisting)
                {
                    item.SetQuantity(item.GetQuantity() - quantity);
                    if (item.GetQuantity() <= 0m)
                    {
                        item.SetQuantity(0);
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
    }
}
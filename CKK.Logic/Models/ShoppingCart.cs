﻿namespace CKK.Logic.Models
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

        public ShoppingCartItem? AddProduct(Product prod)
        {
            return AddProduct(prod, 1);
        }

        public ShoppingCartItem? GetProductById(int id)
        {
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

        public ShoppingCartItem? AddProduct(Product prod, int quantity)
        {
            ShoppingCartItem newItem = new(prod, quantity);
            var CheckForExisting =
                from e in _Products
                where prod == e.GetProduct()
                select e;
            if (quantity <= 0m)
            {
                return null;
            }
            if (CheckForExisting.Any())
            {
                foreach (var item in CheckForExisting)
                {
                    item.SetQuantity(quantity + item.GetQuantity());
                    return item;
                }
            }
            else _Products.Add(newItem);
            return newItem;
        }

        public ShoppingCartItem? RemoveProduct(int id, int quantity)
        {
            var CheckForExisting =
                from e in _Products
                where id == e.GetProduct().GetId()
                select e;
            if (CheckForExisting.Any())
            {
                foreach (var item in CheckForExisting)
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
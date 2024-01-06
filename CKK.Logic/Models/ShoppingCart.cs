﻿using CKK.Logic.Interfaces;
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
            var Product = Existing.First();
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
            var Product = existing.First();
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
            var Product = Existing.First() ?? throw new ProductDoesNotExistException();
            if( id < 0 ) {
                throw new InvalidIdException();
            }
            if( quant < 0 ) {
                throw new InventoryItemStockTooLowException();
            }
            Product.Quantity -= quant;
            if( Product.Quantity < 0 ) {
                Product.Quantity = 0;
                Products.Remove(Product);

            }
            return Product;
        }

        public decimal GetTotal() {
            var GetTotal =
                from e in Products
                select e;
            decimal Total = 0m;
            foreach(var item in GetTotal) {
                Total += item.GetTotal();
            }
            return Total;
        }
    }
}
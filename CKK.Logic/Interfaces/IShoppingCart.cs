using CKK.Logic.Exceptions;
using CKK.Logic.Models;
using System.ComponentModel.Design;

namespace CKK.Logic.Interfaces {
    public interface IShoppingCart {
        public int GetCustomerId();
        public List<ShoppingCartItem> GetProducts();
        public ShoppingCartItem? GetProductById(int id);
        public ShoppingCartItem AddProduct(Product prod, int quant);
        public ShoppingCartItem? RemoveProduct(int id, int quant);
        public decimal GetTotal();
    }
}

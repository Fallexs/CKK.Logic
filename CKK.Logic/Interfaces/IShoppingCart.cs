using CKK.Logic.Exceptions;
using CKK.Logic.Models;
using System.ComponentModel.Design;

namespace CKK.Logic.Interfaces {
    public interface IShoppingCart {
        int GetCustomerId();
        List<ShoppingCartItem> GetProducts();
        ShoppingCartItem? GetProductById(int id);
        ShoppingCartItem AddProduct(Product prod, int quant);
        ShoppingCartItem? RemoveProduct(int id, int quant);
        decimal GetTotal();
    }
}

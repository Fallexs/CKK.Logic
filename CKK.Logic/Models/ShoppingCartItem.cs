﻿namespace CKK.Logic.Models {
    public class ShoppingCartItem {
        private Product _product;
        private int _quantity;
        public int GetQuantity() => _quantity;
        public Product GetProduct() => _product;
        public void SetQuantity(int quantity) => _quantity = quantity;
        public void SetProduct(Product product) => _product = product;
        public decimal GetTotal() => _quantity * _product.GetPrice();

        public ShoppingCartItem(Product product, int quantity) {
            _product = product;
            _quantity = quantity;
        }
    }
}
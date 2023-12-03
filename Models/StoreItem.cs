namespace CKK.Logic {
    namespace Models {
        public class StoreItem {
            private Product _product;
            private int _quantity;
            public int GetQuantity() => _quantity;
            public Product GetProduct() => _product;

            public StoreItem(Product product, int quantity) {
                _product = product;
                _quantity = quantity;
            }

            public void SetQuantity(int quantity) {
                _quantity = quantity;
            }

            public void SetProduct(Product product) {
                _product = product;
            }
        }
    }
}

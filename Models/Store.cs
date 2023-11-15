namespace Models {
    public class Store {
        private int _Id;
        private string? _name;
        private Product? _product1;
        private Product? _product2;
        private Product? _product3;

        public int GetId() => _Id;
        public string GetName() => _name ?? "Null";

        public void SetId(int id) => _Id = id;
        public void SetName(string name) => _name = name;

        public void AddStoreItem(Product prod) {
            if( _product1 == null ) {
                _product1 = prod;
            }
            else if( _product2 == null ) {
                _product2 = prod;
            }
            else if( _product3 == null ) {
                _product3 = prod;
            }
        }

        public void RemoveStoreItem(int productNumber) {
            if (productNumber == 1 && _product1 != null) {
                _product1 = null;
            }
            else if(productNumber == 2 && _product2 != null) {
                _product2 = null;
            }
            else if(productNumber == 3 && _product3 != null) {
                _product3 = null;
            }
        }

        public Product? GetStoreItem(int productNumber) {
            if (productNumber == 1 && _product1 != null) {
                return _product1;
            } else if (productNumber == 2 && _product2 != null) {
                return _product2;
            } else if (productNumber == 3 && _product3 != null) {
                return _product3;
            }
            return null;
        }

        public Product? GetStoreItemById(int id) {
            if (_product1?.GetId() == id) {
                return _product1;
            } else if (_product2?.GetId() == id) {
                return _product2;
            } else if (_product3?.GetId() == id) {
                return _product3;
            } return null;
        }
    }
}

namespace CKK.Logic {
    public class Product {
        private int _Id;
        private string? _name;
        private decimal _price;

        public int GetId() => _Id;
        public string GetName() => _name ?? "Null";
        public decimal GetPrice() => _price;

        public void SetId(int id) {
            _Id = id;
        }

        public void SetName(string name) {
            _name = name;
        }

        public void SetPrice(decimal price) {
            _price = price;
        }
    }
}

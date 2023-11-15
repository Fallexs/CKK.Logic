namespace CKK.Logic.Models {
    public class Product {
        private int _id;
        private string? _name;
        private decimal _price;

        public int GetId() => _id;
        public string GetName() => _name ?? "Null";
        public decimal GetPrice() => _price;

        public void SetId(int id) {
            _id = id;
        }

        public void SetName(string name) {
            _name = name;
        }

        public void SetPrice(decimal price) {
            _price = price;
        }
    }
}

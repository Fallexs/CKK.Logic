namespace CKK.Logic.Models {
    public class Customer {
        private int _id;
        private string? _name;
        private string? _address;

        public int GetId() => _id;
        public string? GetName() => _name;
        public string? GetAddress() => _address;

        public void SetId(int id) {
            _id = id;
        }

        public void SetName(string name) {
            _name = name;
        }

        public void SetAddress(string address) {
            _address = address;
        }
    }
}

﻿namespace CKK.Logic {
    public class Customer {
        private int _Id;
        private string? _name;
        private string? _address;

        public int GetId() => _Id;
        public string GetName() => _name ?? "Null";
        public string GetAddress() => _address ?? "Null";

        public void SetId(int id) {
            _Id = id;
        }

        public void SetName(string name) {
            _name = name;
        }

        public void SetAddress(string address) {
            _address = address;
        }
    }
}

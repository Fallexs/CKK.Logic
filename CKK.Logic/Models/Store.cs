namespace CKK.Logic.Models {
    public class Store {
        private int _id;
        private string? _name;
        private List<StoreItem> _items = new();
        public int GetId() => _id;
        public string GetName() => _name ?? "Null";
        public void SetId(int id) => _id = id;
        public void SetName(string name) => _name = name;
        public List<StoreItem> GetStoreItems() => _items;

        public StoreItem? AddStoreItem(Product prod, int quantity) {
            var SameStoreItem =
                from e in _items
                where prod == e.GetProduct()
                select e;
            if( prod != null && quantity > 0 ) {
                if( SameStoreItem.Any() ) {
                    foreach( StoreItem item in _items ) {
                        item.SetQuantity(item.GetQuantity() + quantity);
                        return item;
                    }
                } else {
                    StoreItem item = new(prod, quantity);
                    _items.Add(item);
                    return item;
                }
            }
            return null;
        }

        public StoreItem? RemoveStoreItem(int id, int quantity) {
            var SameStoreItem =
                from e in _items
                where id == e.GetProduct().GetId()
                select e;
            if( SameStoreItem.Any() ) {
                foreach(var item in SameStoreItem) {
                    item.SetQuantity(item.GetQuantity() - quantity);
                    if(item.GetQuantity() < 0) {
                        item.SetQuantity(0);
                    }
                    return item;
                }
            }
            return null;
        }

        public StoreItem? FindStoreItemById(int id) {
            var FindByID =
                from e in _items
                where id == e.GetProduct().GetId()
                select e;
            if( FindByID.Any() ) {
                foreach(var item in FindByID) {
                    return item;
                }
            }
            return null;
        }
    }
}

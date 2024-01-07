using CKK.Logic.Interfaces;

namespace CKK.Logic.Models {
    public class StoreItem : InventoryItem {
        public StoreItem(Product? product, int quantity) {
            Product = product;
            Quantity = quantity;
        }

        public StoreItem() { }
    }
}

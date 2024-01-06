using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models {
    public class Store : Entity, IStore {
        private List<StoreItem> Items { get; set; } = new();

        public StoreItem? AddStoreItem(Product prod, int quantity) {
            try {
                var FindExisting =
                    from e in Items
                    where prod == e.Product
                    select e;
                if( quantity <= 0 ) {
                    throw new InventoryItemStockTooLowException();
                }
                if( FindExisting.Any() ) {
                    foreach( StoreItem item in Items ) {
                        item.Quantity += quantity;
                        return item;
                    }
                }
                StoreItem newStoreItem = new(prod, quantity);
                Items.Add(newStoreItem);
                return newStoreItem;
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public StoreItem? RemoveStoreItem(int id, int quantity) {
            try {
                var SameStoreItem =
                    from e in Items
                    where id == e.Product.Id
                    select e;
                if ( quantity < 0) {
                    throw new ArgumentOutOfRangeException(nameof(quantity));
                }
                if(SameStoreItem.Any()) {
                    foreach( StoreItem item in SameStoreItem ) {
                        item.Quantity -= quantity;
                        if( item.Quantity < 0 ) {
                            item.Quantity = 0;
                        }
                        return item;
                    }
                }
                throw new ProductDoesNotExistException();
            } catch( Exception ex ) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public StoreItem? FindStoreItemById(int id) {
            var FindByID =
                from e in Items
                where id == e.Product.Id
                select e;
            if( FindByID.Any() ) {
                foreach( StoreItem item in FindByID ) {
                    return item;
                }
            }
            return null;
        }
        

        public List<StoreItem> GetStoreItems() => Items;
    }
}

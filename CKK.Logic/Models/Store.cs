using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models {
    public class Store : Entity, IStore {
        private List<StoreItem> Items { get; set; } = new();

        public StoreItem AddStoreItem(Product prod, int quantity) {
            var FindExisting =
                from e in Items
                where prod == e.Product
                select e;
            var Item = FindExisting.First();
            try {
                if( quantity <= 0 ) {
                    throw new InventoryItemStockTooLowException();
                }
            } catch ( Exception ex ) { 
                Console.WriteLine(ex.Message);
            }
            if (Item == null) {
                var newItem = new StoreItem(prod, quantity);
                Items.Add(newItem);
                return newItem;
            } else {
                Item.Quantity += quantity;
            }
            return Item;
        }

        public StoreItem RemoveStoreItem(int id, int quantity) {
            var FindExisting =
                from e in Items
                where id == e.Product.Id
                select e;
            var Item = FindExisting.First();
            try {
                if( !FindExisting.Any() ) {
                    throw new ProductDoesNotExistException();
                }
                if ( Item.Quantity - quantity <= 0 ) {
                    throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot be less than or equal to 0.");
                }
            } catch( ProductDoesNotExistException ex ) {
                Console.WriteLine(ex.Message);
            } catch ( ArgumentOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
                Item.Quantity = 0;
                Items.Remove(Item);
                return Item;
            }
            Item.Quantity -= quantity;
            return Item;
        }

        public StoreItem FindStoreItemById(int id) {
            try {
                if( id < 0 ) {
                    throw new InvalidIdException();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            var FindByID =
                from e in Items
                where id == e.Product.Id
                select e;
            return FindByID.First();
        }
        

        public List<StoreItem> GetStoreItems() => Items;
    }
}

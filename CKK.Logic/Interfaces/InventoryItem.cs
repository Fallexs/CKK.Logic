using CKK.Logic.Models;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Interfaces {
    public abstract class InventoryItem {
        private Product product = new();
        private int quantity;

        public int Quantity {
            get {
                return quantity;
            }
            set {
                if( value >= 0 ) {
                    quantity = value;
                } else throw new InventoryItemStockTooLowException();
            }
        }

        public Product Product {
            get {
                return product;
            } set {
                product = value;
            }
        }
    }
}

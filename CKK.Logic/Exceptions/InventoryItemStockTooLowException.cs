using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions {
    public class InventoryItemStockTooLowException : Exception {
        public InventoryItemStockTooLowException() : base("Invalid Item Quantity Entered! Cannot be less than 0.") {
        }
    }
}

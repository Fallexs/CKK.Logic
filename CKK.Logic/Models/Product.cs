using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models {
    public class Product : Entity {
        private decimal price;

        public decimal Price {
            get {
                return price;
            }
            set {
                try {
                    if( value < 0 ) {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }
                    price = value;

                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }
    }
}

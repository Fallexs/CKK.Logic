﻿using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models {
    public class Product : Entity {
        private decimal price;

        public decimal Price {
            get {
                return price;
            }
            set {
                if ( value < 0 ) {
                    throw new ArgumentOutOfRangeException(nameof(Price), "Invalid Amount.");
                }
                price = value;
            }
        }
    }
}

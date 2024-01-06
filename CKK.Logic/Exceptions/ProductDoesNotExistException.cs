using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions {
    public class ProductDoesNotExistException : Exception {
        public ProductDoesNotExistException() : base("Cannot remove product, invalid product entered!") {
        }
    }
}

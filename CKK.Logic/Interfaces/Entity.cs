using CKK.Logic.Exceptions;

namespace CKK.Logic.Interfaces {
    public abstract class Entity {
        private int? id;
        public string Name { get; set; } = string.Empty;

        public int? Id {
            get {
                return id;
            }
            set {
                if( value >= 0 ) {
                    id = value;
                } else throw new InvalidIdException();
                id = null;
            }
        }
    }
}

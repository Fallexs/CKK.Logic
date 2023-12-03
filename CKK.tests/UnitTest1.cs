namespace CKK.tests {
    public class UnitTest1 {
        Product NewProduct = new();
        ShoppingCartItem NewShoppingCartItem = new();
        Product NewProduct2 = new();
        Product NewProduct3 = new();
        ShoppingCart NewShoppingCart = new ShoppingCart();

        [Fact]
        public void PassingAddingProduct() {
            NewShoppingCartItem.SetProduct(NewProduct);
            Assert.Equal(NewProduct, NewShoppingCartItem.GetProduct());
        }

        [Fact]
        public void FailingAddingProduct() {
            NewShoppingCartItem.SetProduct(NewProduct);
            Assert.Equal(NewProduct2, NewShoppingCartItem.GetProduct());
        }


        [Fact]
        public void PassingRemoveProduct() {

            NewShoppingCart.AddProduct(NewProduct);
            NewShoppingCart.RemoveProduct(NewProduct, 1);
            Assert.Null(NewShoppingCartItem.GetProduct());

        }

        [Fact]
        public void FailingRemoveProduct() {

            NewShoppingCart.AddProduct(NewProduct);
            NewShoppingCart.RemoveProduct(NewProduct, 1);
            Assert.Equal(NewProduct, NewShoppingCartItem.GetProduct());

        }

        [Fact]
        public void PassingTotal() {
            NewProduct.SetPrice(40);
            NewProduct2.SetPrice(30);
            NewProduct3.SetPrice(50);
            NewShoppingCart.AddProduct(NewProduct);
            NewShoppingCart.AddProduct(NewProduct2);
            NewShoppingCart.AddProduct(NewProduct3);
            Assert.Equal(120, NewShoppingCart.GetTotal());
        }

        [Fact]
        public void FailingTotal() {
            NewProduct.SetPrice(10);
            NewProduct2.SetPrice(20);
            NewProduct3.SetPrice(30);

            NewShoppingCart.AddProduct(NewProduct);
            NewShoppingCart.AddProduct(NewProduct2);
            NewShoppingCart.AddProduct(NewProduct3);
            Assert.Equal(50, NewShoppingCart.GetTotal());
        }
    }
}

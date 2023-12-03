using CKK.Logic.Models;
<<<<<<< HEAD
namespace CKK.tests
{
    public class UnitTest1
    {
=======
namespace CKK.tests {
    public class UnitTest1 {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
        static Product product = new();
        static Product product2 = new();
        static Product product3 = new();
        static Customer jerry = new Customer();
        ShoppingCart cart = new(jerry);
        ShoppingCartItem item = new(product, 5);

        [Fact]
<<<<<<< HEAD
        public void PassingAddingProduct()
        {
=======
        public void PassingAddingProduct() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            item.SetProduct(product);
            Assert.Same(product, item.GetProduct());
        }

        [Fact]
<<<<<<< HEAD
        public void FailingAddingProduct()
        {
=======
        public void FailingAddingProduct() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            item.SetProduct(product);
            Assert.Equal(product2, item.GetProduct());
        }


        [Fact]
<<<<<<< HEAD
        public void PassingRemoveProduct()
        {
=======
        public void PassingRemoveProduct() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            item.SetProduct(product);
            cart.RemoveProduct(product, 1);
            Assert.Equal(product, item.GetProduct());
        }

        [Fact]
<<<<<<< HEAD
        public void FailingRemoveProduct()
        {
=======
        public void FailingRemoveProduct() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            item.SetProduct(product);
            cart.RemoveProduct(product, 1);
            Assert.Equal(product2, item.GetProduct());
        }

        [Fact]
<<<<<<< HEAD
        public void PassingTotal()
        {
=======
        public void PassingTotal() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            product.SetPrice(40);
            product2.SetPrice(30);
            product3.SetPrice(50);
            cart.AddProduct(product);
            cart.AddProduct(product2);
            cart.AddProduct(product3);
            Assert.Equal(120, cart.GetTotal());
        }

        [Fact]
<<<<<<< HEAD
        public void FailingTotal()
        {
=======
        public void FailingTotal() {
>>>>>>> 6c3b4cd0494bb5066995dd14f620fddb4f713d90
            product.SetPrice(10);
            product2.SetPrice(20);
            product3.SetPrice(30);

            cart.AddProduct(product);
            cart.AddProduct(product2);
            cart.AddProduct(product3);
            Assert.Equal(50, cart.GetTotal());
        }
    }
}



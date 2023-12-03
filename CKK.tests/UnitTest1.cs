using CKK.Logic.Models;
namespace CKK.tests
{
    public class UnitTest1
    {
        static Product product = new();
        static Product product2 = new();
        static Product product3 = new();
        static Customer jerry = new Customer();
        ShoppingCart cart = new(jerry);
        ShoppingCartItem item = new(product, 5);

        [Fact]
        public void PassingAddingProduct()
        {
            item.SetProduct(product);
            Assert.Same(product, item.GetProduct());
        }

        [Fact]
        public void FailingAddingProduct()
        {
            item.SetProduct(product);
            Assert.Equal(product2, item.GetProduct());
        }


        [Fact]
        public void PassingRemoveProduct()
        {
            item.SetProduct(product);
            cart.RemoveProduct(product, 1);
            Assert.Equal(product, item.GetProduct());
        }

        [Fact]
        public void FailingRemoveProduct()
        {
            item.SetProduct(product);
            cart.RemoveProduct(product, 1);
            Assert.Equal(product2, item.GetProduct());
        }

        [Fact]
        public void PassingTotal()
        {
            product.SetPrice(40);
            product2.SetPrice(30);
            product3.SetPrice(50);
            cart.AddProduct(product);
            cart.AddProduct(product2);
            cart.AddProduct(product3);
            Assert.Equal(120, cart.GetTotal());
        }

        [Fact]
        public void FailingTotal()
        {
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



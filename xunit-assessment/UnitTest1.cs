using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace xunit_assessment
{
    public class UnitTest1
    {
        [Fact]
        public void Products_ShouldReturnNullExceptionIfTheValueIsNull()
        {
            //Act and Assert
            Assert.Throws<ArgumentNullException>(
                () => new Products().AddNew(null)
                );
        }

        [Fact]
        public void Products_ShouldReturnTrueIfTheNewProductWasAdded()
        {
            // Arrange
            var product = new Product {
                Name = new Guid().ToString(),
                IsSold = false
            };

            // Act
            var products = new Products();            
            products.AddNew(product);

            // Assert
            Assert.Contains<Product>(product, products.Items);
        }

        [Fact]
        public void Products_ShouldReturnExceptionIfTheProductNameIsNull() {
            //Arrange
            var product = new Product
            {
                Name = null,
                IsSold = false
            };

            // Assert
            Assert.Throws<NameRequiredException>(
                () => new Products().AddNew(product)
                );
        }
    }

    internal class Products
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Items => _products.Where(t => !t.IsSold);

        public void AddNew(Product product)
        {
            product = product ??
                throw new ArgumentNullException();
            product.Validate();
            _products.Add(product);
        }

        public void Sold(Product product)
        {
            product.IsSold = true;
        }

    }

    internal class Product
    {
        public bool IsSold { get; set; }
        public string Name { get; set; }

        internal void Validate()
        {
            Name = Name ??
                throw new NameRequiredException();
        }

    }

    [Serializable]
    internal class NameRequiredException : Exception
    {
        public NameRequiredException() { /* ... */ }

        public NameRequiredException(string message) : base(message) { /* ... */ }

        public NameRequiredException(string message, Exception innerException) : base(message, innerException) { /* ... */ }

        protected NameRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { /* ... */ }
    }
}

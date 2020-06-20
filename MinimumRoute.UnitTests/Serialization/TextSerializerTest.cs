using MinimumRoute.Serialization;
using MinimumRoute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using System.Text.RegularExpressions;

namespace MinimumRoute.UnitTests.Serialization
{
    public class TextSerializerTest
    {
        [Theory]
        [InlineData("LS", "SF")]
        [InlineData("LS", null)]
        public void DeserializeOrder(string cityOrigin, string cityDestination)
        {
            var line = $"{cityOrigin} {cityDestination}";

            var textSerializer = new TextSerializer();
            var order = textSerializer.DeserializeObject<OrderViewModel>(line);

            AssertOrder(cityOrigin, cityDestination, order);
        }

        private static void AssertOrder(string cityOrigin, string cityDestination, OrderViewModel order)
        {
            Assert.Equal(cityOrigin, order.CityOrigin);
            Assert.Equal(cityDestination, order.CityDestination);
        }

        [Theory]
        [InlineData("LS", "SF", 1)]
        public void DeserializeRoute(string cityOrigin, string cityDestination, int distance)
        {
            var line = $"{cityOrigin} {cityDestination} {distance}";

            var textSerializer = new TextSerializer();
            var route = textSerializer.DeserializeObject<RouteViewModel>(line);

            Assert.Equal(cityOrigin, route.CityOrigin);
            Assert.Equal(cityDestination, route.CityDestination);
            Assert.Equal(distance, route.Distance);
        }

        [Fact]
        public void DeserializeListOrder()
        {
            var list = new List<List<string>>
            {
                new List<string> { "LS", "SF" },
                new List<string> { "SF", "LS" },
            };
            var content = string.Join(Environment.NewLine, list.Select(l => string.Join(" ", l)));


            var textSerializer = new TextSerializer();
            var listOrder = textSerializer.DeserializeList<OrderViewModel>(content);

            Assert.Equal(list.Count, listOrder.Count);
            foreach (var (line, i) in list.Select((x, i) => (x, i)))
            {
                AssertOrder(line.First(), line.Last(), listOrder[i]);
            }
        }


        [Theory]
        [InlineData("LS", "SF")]
        [InlineData("LS", null)]
        public void SerializeOrder(string cityOrigin, string cityDestination)
        {
            var order = new OrderViewModel { CityOrigin = cityOrigin, CityDestination = cityDestination };

            var textSerializer = new TextSerializer();
            var content = textSerializer.SerializeObject(order);

            Assert.Equal($"{cityOrigin} {cityDestination}".Trim(), content);
        }

        [Fact]
        public void SerializeISerializer()
        {
            var mockISerializer = new Mock<ISerializer>();
            mockISerializer.Setup(s => s.Serializer(It.IsAny<Func<object, string>>())).Returns("text");

            var textSerializer = new TextSerializer();
            var content = textSerializer.SerializeObject(mockISerializer.Object);

            Assert.Equal("text", content);
            mockISerializer.Verify(s => s.Serializer(It.IsAny<Func<object, string>>()), Times.Once);
        }

        [Fact]
        public void SerializeListOrder()
        {
            var list = new List<OrderViewModel>
            {
                new OrderViewModel { CityOrigin = "LS",CityDestination = "SF" },
                new OrderViewModel { CityOrigin= "SF", CityDestination = "LS" },
            };

            var textSerializer = new TextSerializer();
            var content = textSerializer.SerializeList(list);

            Assert.Equal(list.Count-1, Regex.Matches(content, Environment.NewLine).Count);
        }
    }
}

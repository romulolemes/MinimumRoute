using MinimumRoute.Data;
using MinimumRoute.Model;
using MinimumRoute.Service;
using MinimumRoute.ViewModel;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MinimumRoute.UnitTests.Service
{
    public class RouteServiceTest
    {
        private readonly RouteService routeService;
        private readonly Mock<IList<RouteEntity>> mockListRoute;
        private readonly CityEntity cityExpected1;
        private readonly CityEntity cityExpected2;

        public RouteServiceTest()
        {
            cityExpected1 = new CityEntity("Los Santos", "LS");
            cityExpected2 = new CityEntity("San Fierro", "SF");

            mockListRoute = new Mock<IList<RouteEntity>>();

            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.Routes).Returns(mockListRoute.Object);

            var mockCityService = new Mock<CityService>(mockContext.Object);
            mockCityService.Setup(c => c.FindByCode(cityExpected1.Code)).Returns(cityExpected1);
            mockCityService.Setup(c => c.FindByCode(cityExpected2.Code)).Returns(cityExpected2);

            routeService = new RouteService(mockContext.Object, mockCityService.Object);
        }

        [Fact]
        public void CreateListRoutes()
        {
            var listRoute = new List<RouteViewModel>
            {
                new RouteViewModel(cityExpected1.Code, cityExpected2.Code, 1),
                new RouteViewModel(cityExpected2.Code, cityExpected1.Code, 2)
            };

            routeService.CreateListRoutes(listRoute);

            mockListRoute.Verify(c => c.Add(It.IsAny<RouteEntity>()), Times.Exactly(listRoute.Count));
        }


        [Fact]
        public void CreateRoute()
        {
            var distance = 1;
            var routeEntity = routeService.CreateRoute(new RouteViewModel(cityExpected1.Code, cityExpected2.Code, distance));

            Assert.Equal(cityExpected1, routeEntity.CityOrigin);
            Assert.Equal(cityExpected2, routeEntity.CityDestination);
            Assert.Equal(distance, routeEntity.Distance);
        }

    }


}

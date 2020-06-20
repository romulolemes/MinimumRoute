using MinimumRoute.Data;
using MinimumRoute.Exceptions;
using MinimumRoute.Model;
using MinimumRoute.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MinimumRoute.UnitTests.Service
{
    public class CityServiceTest
    {
        private CityEntity cityExpected;
        private CityService _cityService;

        public CityServiceTest()
        {
            cityExpected = new CityEntity("Los Santos", "LS");

            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.Cities).Returns(new List<CityEntity> { cityExpected });

            _cityService = new CityService(mockContext.Object);
        }

        [Fact]
        public void ReturnCityEntityWhenPassValidCityCode()
        {
            var _cityActual = _cityService.FindByCode(cityExpected.Code);

            Assert.Equal(cityExpected, _cityActual);
        }

        [Fact]
        public void ThorwNotFoundWhenCityCodeDoesNotExist()
        {
            Assert.Throws<EntityNotFoundException>(() => _cityService.FindByCode("unknown"));
        }
    }
}

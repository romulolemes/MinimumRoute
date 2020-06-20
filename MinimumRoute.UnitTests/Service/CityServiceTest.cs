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
        private readonly CityEntity cityExpected;
        private readonly CityService _cityService;
        private readonly Mock<Context> _mockContext;

        public CityServiceTest()
        {
            cityExpected = new CityEntity("Los Santos", "LS");

            _mockContext = new Mock<Context>();
            _mockContext.Setup(c => c.Cities).Returns(new List<CityEntity> { cityExpected });

            _cityService = new CityService(_mockContext.Object);
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

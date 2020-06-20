using MinimumRoute.Model;
using MinimumRoute.Search.Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MinimumRoute.UnitTests.Search.Dijkstra
{
    public class DijkstraAlgorithmTest
    {
        readonly CityEntity cityLS;
        readonly CityEntity citySF;
        readonly CityEntity cityLV;
        readonly CityEntity cityRC;
        readonly CityEntity cityWS;
        readonly CityEntity cityBC;
        readonly CityEntity cityNF;
        readonly IEnumerable<RouteEntity> routes;

        public DijkstraAlgorithmTest()
        {
            cityLS = new CityEntity("Los Santos", "LS");
            citySF = new CityEntity("San Fierro", "SF");
            cityLV = new CityEntity("Las Venturas", "LV");
            cityRC = new CityEntity("Red County", "RC");
            cityWS = new CityEntity("Whetstone", "WS");
            cityBC = new CityEntity("Bone County", "BC");
            cityNF = new CityEntity("Not Found", "NF");

            routes = new List<RouteEntity>
            {
                new RouteEntity (cityLS, citySF, 1),
                new RouteEntity (citySF, cityLS, 2),
                new RouteEntity (cityLS, cityLV, 1),
                new RouteEntity (cityLV, cityLS, 1),
                new RouteEntity (citySF, cityLV, 2),
                new RouteEntity (cityLV, citySF, 2),
                new RouteEntity (cityLS, cityRC, 1),
                new RouteEntity (cityRC, cityLS, 2),
                new RouteEntity (citySF, cityWS, 1),
                new RouteEntity (cityWS, citySF, 2),
                new RouteEntity (cityLV, cityBC, 1),
                new RouteEntity (cityBC, cityLV, 1),
            };
        }

        [Fact]
        public void ShoertPathSFtoWS()
        {
            var dijkstraAlgorithmt = new DijkstraAlgorithm();
            var path = dijkstraAlgorithmt.FindShortestPath(citySF, cityWS, GetNeighboardhood(routes));


            Assert.Equal(1, path.Distance);
            List<CityEntity> nodeVisitExpected = new List<CityEntity> { citySF, cityWS };
            Assert.Equal(nodeVisitExpected, path.NodeVisit);
        }


        [Fact]
        public void ShoertPathLStoBC()
        {
            var dijkstraAlgorithmt = new DijkstraAlgorithm();
            var path = dijkstraAlgorithmt.FindShortestPath(cityLS, cityBC, GetNeighboardhood(routes));


            Assert.Equal(2, path.Distance);
            List<CityEntity> nodeVisitExpected = new List<CityEntity> { cityLS, cityLV, cityBC };
            Assert.Equal(nodeVisitExpected, path.NodeVisit);
        }

        [Fact]
        public void ShoertPathWStoBC()
        {
            var dijkstraAlgorithmt = new DijkstraAlgorithm();
            var path = dijkstraAlgorithmt.FindShortestPath(cityWS, cityBC, GetNeighboardhood(routes));


            Assert.Equal(5, path.Distance);
            List<CityEntity> nodeVisitExpected = new List<CityEntity> { cityWS, citySF, cityLV, cityBC };
            Assert.Equal(nodeVisitExpected, path.NodeVisit);
        }

        [Fact]
        public void ShoertPathWithoutPath()
        {
            var dijkstraAlgorithmt = new DijkstraAlgorithm();
            var path = dijkstraAlgorithmt.FindShortestPath(cityLS, cityNF, GetNeighboardhood(routes));


            Assert.Null(path.Distance);
            List<CityEntity> nodeVisitExpected = new List<CityEntity>();
            Assert.Equal(nodeVisitExpected, path.NodeVisit);
        }

        private static Func<Node, IEnumerable<NeighborhoodInfo>> GetNeighboardhood(IEnumerable<RouteEntity> routes)
        {
            return n => Runner.GetNeighbors(n, routes);
        }
    }
}

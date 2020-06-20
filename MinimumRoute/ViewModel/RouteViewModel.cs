namespace MinimumRoute.ViewModel
{
    public class RouteViewModel
    {
        public RouteViewModel()
        {

        }

        public RouteViewModel(string cityOrigin, string cityDestination, int distance)
        {
            CityOrigin = cityOrigin;
            CityDestination = cityDestination;
            Distance = distance;
        }

        public string CityOrigin { get; set; }
        public string CityDestination { get; set; }
        public int Distance { get; set; }
    }
}

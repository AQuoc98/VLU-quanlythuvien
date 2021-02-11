namespace CoreApp.Common.Models
{
    public class Coordinate
    {
        public double Lat { get; set; }
        public double Long { get; set; }

        public Coordinate(double latitude, double longitude)
        {
            Lat = latitude;
            Long = longitude;
        }
    }
}

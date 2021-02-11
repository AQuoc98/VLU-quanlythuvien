using CoreApp.Common.Models;
using System;
using System.Net;
using System.Web;

namespace CoreApp.Common.Helpers
{
    public static class GeoLocationHelper
    {
        private const string _googleUri = "https://maps.googleapis.com/maps/api/geocode";
        private const string _googleKey = "AIzaSyAMy3LQKeOuejRY42VXLSEySfQlrIONlgs";
        private const string _outputType = "xml"; // Available options: csv, xml, kml, json

        /// <summary>
        /// Get coordinates of address
        /// </summary>
        /// <param name="address"></param>
        /// <returns>A spatial coordinate that contains the latitude and longitude of the address.</returns>
        public static Coordinate GetCoordinates(string address)
        {

            WebClient client = new WebClient();
            Uri uri = GetGeocodeUri(address);

            // The first number is the status code,
            // the second is the accuracy,
            // the third is the latitude,
            // the fourth one is the longitude.
            var geocodeInfo = client.DownloadString(uri);

            var strLat = XmlHelper.GetValue(geocodeInfo, "lat");
            var strLong = XmlHelper.GetValue(geocodeInfo, "lng");


            var latitude = double.Parse(strLat);
            var longitude = double.Parse(strLong);

            return new Coordinate(latitude, longitude);
        }

        /// <summary>
        /// Calculate distance form 2 location by Coordinates
        /// </summary>
        /// <param name="fromPlace"></param>
        /// <param name="toPlace"></param>
        /// <returns>Distance in Km</returns>
        public static double DistanceFromLatLongInKm(Coordinate fromPlace, Coordinate toPlace)
        {
            const int eR = 6371; // This is radius of the earn in Km

            var dLat = Deg2Rad(toPlace.Lat - fromPlace.Lat);
            var dLong = Deg2Rad(toPlace.Long - fromPlace.Long);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Deg2Rad(fromPlace.Lat)) * Math.Cos(Deg2Rad(toPlace.Lat)) *
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = eR * c; // Distance in Km

            return Math.Round(d,1);
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        private static Uri GetGeocodeUri(string address)
        {
            address = HttpUtility.UrlEncode(address);
            return new Uri($"{_googleUri}/{_outputType}?address={address}&key={_googleKey}");
        }
    }
}

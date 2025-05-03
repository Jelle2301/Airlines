using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Hotel
{
    public class HotelResponseApi
    {
        public List<Hotel>? Data { get; set; }
        public Meta? Meta { get; set; }
    }

    public class Hotel
    {
        public string? ChainCode { get; set; }
        public string? IataCode { get; set; }
        public int DupeID { get; set; }
        public string? Name { get; set; }
        public string? HotelID { get; set; }
        public GeoCode? GeoCode { get; set; }
        public Address? Address { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

    public class GeoCode
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Address
    {
        public string? CountryCode { get; set; }
    }

    public class Meta
    {
        public long Count { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public string? Self { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenjaminSchroeter.GeoNames;

namespace GeoNamesXMLBuilder.Model
{
    public class SimpleGeoName
    {
        public SimpleGeoName()
        {
            Children = new List<SimpleGeoName>();
        }
        public SimpleGeoName(Geoname geoname)
        {
            Id = geoname.Id;
            Name = geoname.Name;
            FCode = geoname.Fcode;
            CountryCode = geoname.CountryCode;
            
            Children = new List<SimpleGeoName>();

            LocalNameES = geoname.AlternateNames().Where(x => x.Language == "es").LastOrDefault().Name;
            LocalNameEN = geoname.AlternateNames().Where(x => x.Language == "en").LastOrDefault().Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<SimpleGeoName> Children { get; set; }
        public string FCode { get; set; }
        public string CountryCode { get; set; }

        public string LocalNameEN { get; set; }
        public string LocalNameES { get; set; }
    }
}

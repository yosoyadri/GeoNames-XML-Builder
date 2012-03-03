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
            this.Children = new List<SimpleGeoName>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<SimpleGeoName> Children { get; set; }
    }
}

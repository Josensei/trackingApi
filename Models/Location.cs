using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;
using System.Text.RegularExpressions;

namespace trackingApi.Models
{
    public class Location
    {
        Regex gpsrx = new Regex(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$");
        public DateTime CreationDate { get; set; }   
        public double Long { get; set; }
        public double Lat { get; set; }

        public Location(string gps) { 
            if(!gpsrx.IsMatch(gps))
            {
                throw new WrongFormatException("La cadena introducida no cumple el formato de coordenadas GPS");
            }
            string[] coordenadas = gps.Split(',');
            //this.Lat = double.Parse(coordenadas[0]);
            this.Lat = double.Parse(coordenadas[0], NumberStyles.Any, CultureInfo.InvariantCulture);
            this.Long = double.Parse(coordenadas[1], NumberStyles.Any, CultureInfo.InvariantCulture);
            //this.Lat = Convert.ToDouble(coordenadas[0]);
            //this.Long = Convert.ToDouble(coordenadas[1]);

        }
    }
}
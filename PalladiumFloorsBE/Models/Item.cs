using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Item.Models
{
    public class Item
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string cashPrice { get; set; }
        public string thickness { get; set; }
        public string catalogId {get; set;}
        public string thumbnailImage { get; set; }

        public string? surface { get; set; }
        public string? finish { get; set; }
        public string? width { get; set; }
        public string? length { get; set; }

        [BsonElement("sf/box")]
        public double? sfPerbox { get; set; }
        public string? nonCashPrice { get; set; }
        public string? dimension { get; set; }
        public string? underpadAttached { get; set; } 
        public string? veneer { get; set; }
    }
}

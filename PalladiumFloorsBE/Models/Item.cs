using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Item.Models
{
    class Item
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string cashPrice { get; set; }
        public string thickness { get; set; }
    }

    class FloorItem : Item
    {
        public string surface { get; set; }
        public string finish { get; set; }
        public string width { get; set; }
        public string length { get; set; }

        [BsonElement("sf/box")]
        public double sfPerbox { get; set; }
        public string nonCashPrice { get; set; } = string.Empty;
    }


    class Molding : Item
    {
        public string dimension { get; set; } = string.Empty;
    }

    class VinylItem: FloorItem
    {

    }

    class HardwoodItem: FloorItem
    {

    }

    class LaminatedItem: FloorItem
    {
        public string underpadAttached { get; set; } = string.Empty;
    }

    class EngineeredItem : FloorItem
    {
        public string veneer { get; set; } = string.Empty;
    }
}

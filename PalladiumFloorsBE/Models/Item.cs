using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Item.Models
{
    class Item
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CashPrice { get; set; }
        public string Thickness { get; set; }
    }

    class FloorItem : Item
    {
        public string Surface { get; set; }
        public string Finish { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }

        [BsonElement("sf/box")]
        public double SfPerBox { get; set; }
        public string NonCashPrice { get; set; } = string.Empty;
    }


    class Molding : Item
    {
        public string Dimension { get; set; } = string.Empty;
    }

    class VinylItem: FloorItem
    {

    }

    class HardwoodItem: FloorItem
    {

    }

    class LaminatedItem: FloorItem
    {

    }

    class EngineeredItem : FloorItem
    {
        public string Veneer { get; set; } = string.Empty;
    }
}

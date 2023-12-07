using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Item
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string surface { get; set; } = string.Empty;
    public string finish { get; set; } = string.Empty;
    public string thickness { get; set; } = string.Empty;
    public string width { get; set; } = string.Empty;
    public string length { get; set; } = string.Empty;
    // public string veneer { get; set; }
    
    [BsonElement("sf/box")]
    public double sfPerBox { get; set; } = 0.0;
    public string cashPrice { get; set; } = string.Empty;
    public string nonCashPrice { get; set; } = string.Empty;
}

public class EngineeredItem : Item
{
    public string veneer { get; set; } = string.Empty;
}




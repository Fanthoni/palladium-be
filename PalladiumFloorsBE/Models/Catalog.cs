using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CatalogItem
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}




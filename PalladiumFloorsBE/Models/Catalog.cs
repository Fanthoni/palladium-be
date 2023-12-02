using MongoDB.Driver;
using MongoDB.Bson;


public class CatalogItem
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Catalog {
    private readonly IMongoCollection<CatalogItem> _catalogItems;

    public Catalog(IMongoDatabase database) {
        _catalogItems = database.GetCollection<CatalogItem>("CatalogItems");
    }

    public List<CatalogItem> GetCatalogs() {
        var filter = Builders<CatalogItem>.Filter.Empty;
        return _catalogItems.Find(filter).ToList();
    }
}


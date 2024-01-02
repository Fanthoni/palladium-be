using Item.Models;
using MongoDB.Driver;

public class CatalogController 
{
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Represents a controller for managing the catalog of items.
    /// </summary>
    public CatalogController(IMongoDatabase database)
    {
        _database = database;
    }

    /// <summary>
    /// Retrieves a list of catalog items.
    /// </summary>
    /// <returns>A list of catalog items.</returns>
    public List<CatalogItem> GetCatalogs() {
        var filter = Builders<CatalogItem>.Filter.Empty;
        return _database.GetCollection<CatalogItem>("CatalogItems").Find(filter).ToList();
    }

    /// <summary>
    /// Retrieves a list of items from the specified catalog.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog.</param>
    /// <returns>A list of items.</returns>
    public List<Item.Models.Item> GetCatalogItems(string catalogId)
    {
        var filter = Builders<Item.Models.Item>.Filter.Eq("catalogId", catalogId);
        return _database.GetCollection<Item.Models.Item>("Items").Find(filter).ToList();
    }

    /// <summary>
    /// Retrieves the categories for a given catalog.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog.</param>
    /// <returns>A list of category names.</returns>
    public List<string> GetCatalogCategories(string catalogId)
    {
        var filter = Builders<Item.Models.Item>.Filter.Eq("catalogId", catalogId);
        return _database.GetCollection<Item.Models.Item>("Items").Find(filter).ToList().Select(item => item.category).Distinct().ToList();
    }

    
    /// <summary>
    /// Uploads the thumbnail image for an item in the catalog.
    /// </summary>
    /// <param name="itemId">The ID of the item.</param>
    /// <param name="catalogName">The name of the catalog.</param>
    /// <param name="thumbnail">The thumbnail image to upload.</param>
    public void UploadItemThumbnail(string itemId, string thumbnail)
    {
        
        var filter = Builders<Item.Models.Item>.Filter.Eq("id", itemId);
        var update = Builders<Item.Models.Item>.Update.Set("thumbnailImage", thumbnail);
        _database.GetCollection<Item.Models.Item>("Items").UpdateOne(filter, update);

        return;
    }
}
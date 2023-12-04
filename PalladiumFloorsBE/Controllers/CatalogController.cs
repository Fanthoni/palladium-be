using MongoDB.Driver;

/// <summary>
/// Represents a controller for managing catalog items.
/// </summary>
public class CatalogController {
    private readonly IMongoCollection<CatalogItem> _catalogItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogController"/> class.
    /// </summary>
    /// <param name="database">The MongoDB database.</param>
    public CatalogController(IMongoDatabase database) {
        _catalogItems = database.GetCollection<CatalogItem>("CatalogItems");
    }
    
    /// <summary>
    /// Retrieves a list of catalog items.
    /// </summary>
    /// <returns>A list of catalog items.</returns>
    public List<CatalogItem> GetCatalogs() {
        var filter = Builders<CatalogItem>.Filter.Empty;
        return _catalogItems.Find(filter).ToList();
    }

    /// <summary>
    /// Retrieves the name of a catalog item based on its ID.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog item.</param>
    /// <returns>The name of the catalog item.</returns>
    private string _GetCatalogName(string catalogId) {
        var filter = Builders<CatalogItem>.Filter.Eq("Id", catalogId);
        var catalog = _catalogItems.Find(filter).FirstOrDefault();
        return catalog.Name;
    }

    /// <summary>
    /// Retrieves the appropriate collection based on the catalog name.
    /// </summary>
    /// <param name="catalogName">The name of the catalog.</param>
    /// <param name="database">The MongoDB database.</param>
    /// <returns>The collection of items based on the catalog name.</returns>
    private IMongoCollection<Item> _GetCategoryItemColllection(IMongoDatabase database, string catalogName)  {
        switch(catalogName) {
            case "Hardwood":
                return database.GetCollection<Item>("HardwoodItems");
            case "Laminate":
                return database.GetCollection<Item>("LaminateItems");
            case "Vinyl":
                return database.GetCollection<Item>("VinylItems");
            case "Laminated":
                return database.GetCollection<Item>("LaminatedItems");
            case "Moulding":
                return database.GetCollection<Item>("MouldingItems");
            default:
                return database.GetCollection<Item>("Items");
        }
    }

    /// <summary>
    /// Retrieves the list of catalog categories for a given catalog ID.
    /// </summary>
    /// <param name="database">The MongoDB database.</param>
    /// <param name="catalogId">The ID of the catalog.</param>
    /// <returns>The list of catalog categories.</returns>
    public List<string> GetCatalogCategories(IMongoDatabase database, string catalogId) {
        string _catalogName = _GetCatalogName(catalogId);
        IMongoCollection<Item> _collection = _GetCategoryItemColllection(database, _catalogName);
        var filter = Builders<Item>.Filter.Empty;
        return _collection.Find(filter).ToList().Select(item => item.category).Distinct().ToList();
    }
}
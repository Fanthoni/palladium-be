using Item.Models;
using MongoDB.Driver;

/// <summary>
/// Represents a controller for managing catalog items.
/// </summary>
public class CatalogController {
    private readonly IMongoCollection<CatalogItem> _catalogItems;
    private readonly IMongoDatabase _database;


    private readonly IMongoCollection<Molding> _moldingItems;
    private readonly IMongoCollection<VinylItem> _vinylItems;
    private readonly IMongoCollection<HardwoodItem> _hardwoodItems;
    private readonly IMongoCollection<LaminatedItem> _laminatedItems;
    private readonly IMongoCollection<EngineeredItem> _engineeredItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogController"/> class.
    /// </summary>
    /// <param name="database">The MongoDB database.</param>
    public CatalogController(IMongoDatabase database) {
        _catalogItems = database.GetCollection<CatalogItem>("CatalogItems");
        _database = database;

        _moldingItems = database.GetCollection<Molding>("MoldingItems");
        _vinylItems = database.GetCollection<VinylItem>("VinylItems");
        _hardwoodItems = database.GetCollection<HardwoodItem>("HardwoodItems");
        _laminatedItems = database.GetCollection<LaminatedItem>("LaminatedItems");
        _engineeredItems = database.GetCollection<EngineeredItem>("EngineeredItems");
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
        var filter = Builders<CatalogItem>.Filter.Eq("id", catalogId);
        var catalog = _catalogItems.Find(filter).FirstOrDefault();
        return catalog.name;
    }

    /// <summary>
    /// Retrieves the appropriate collection based on the catalog name.
    /// </summary>
    /// <param name="catalogName">The name of the catalog.</param>
    /// <param name="database">The MongoDB database.</param>
    /// <returns>The collection of items based on the catalog name.</returns>
    public List<string> GetCatalogCategories(string catalogId)
    {
        string catalogName = _GetCatalogName(catalogId);
        List<Item.Models.Item> items = new List<Item.Models.Item>();

        switch (catalogName)
        {
            case "Hardwood":
                items = _hardwoodItems.Find(Builders<HardwoodItem>.Filter.Empty).ToList().Cast<Item.Models.Item>().ToList();
                break;
            case "Vinyl":
                items = _vinylItems.Find(Builders<VinylItem>.Filter.Empty).ToList().Cast<Item.Models.Item>().ToList();
                break;
            case "Laminated":
                items = _laminatedItems.Find(Builders<LaminatedItem>.Filter.Empty).ToList().Cast<Item.Models.Item>().ToList();
                break;
            case "Engineered":
                items = _engineeredItems.Find(Builders<EngineeredItem>.Filter.Empty).ToList().Cast<Item.Models.Item>().ToList();
                break;
            case "Moulding":
                items = _moldingItems.Find(Builders<Molding>.Filter.Empty).ToList().Cast<Item.Models.Item>().ToList();
                break;
        }

        return items.Select(item => item.category).Distinct().ToList();
    }

    /// <summary>
    /// Retrieves a list of catalog items based on the specified catalog ID.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog.</param>
    /// <returns>A list of catalog items.</returns>
    public List<object> GetCatalogItems(string catalogId)
    {
        string catalogName = _GetCatalogName(catalogId);
        List<object> items = new List<object>();

        switch (catalogName)
        {
            case "Hardwood":
                items = _hardwoodItems.Find(Builders<HardwoodItem>.Filter.Empty).ToList().Cast<object>().ToList();
                break;
            case "Vinyl":
                items = _vinylItems.Find(Builders<VinylItem>.Filter.Empty).ToList().Cast<object>().ToList();
                break;
            case "Laminated":
                items = _laminatedItems.Find(Builders<LaminatedItem>.Filter.Empty).ToList().Cast<object>().ToList();
                break;
            case "Engineered":
                items = _engineeredItems.Find(Builders<EngineeredItem>.Filter.Empty).ToList().Cast<object>().ToList();
                break;
            case "Moulding":
                items = _moldingItems.Find(Builders<Molding>.Filter.Empty).ToList().Cast<object>().ToList();
                break;
        }

        return items;
    }
}
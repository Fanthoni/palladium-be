using Item.Models;
using MongoDB.Driver;

public class CatalogController 
{
    private interface IItemFinder
    {
        /// <summary>
        /// Finds and returns a list of items.
        /// </summary>
        /// <returns>A list of items.</returns>
        List<object> FindItems();

        /// <summary>
        /// Finds the categories.
        /// </summary>
        /// <returns>A list of strings representing the categories.</returns>
        List<string> FindCategories();
    }

    private class HardwoodItemFinder : IItemFinder
    {
        private readonly IMongoCollection<HardwoodItem> _hardwoodItems;

        public HardwoodItemFinder(IMongoDatabase database) 
        {
            _hardwoodItems = database.GetCollection<HardwoodItem>("HardwoodItems");
        }

        public List<string> FindCategories()
        {
            return _hardwoodItems.Find(Builders<HardwoodItem>.Filter.Empty).ToList().Select(item => item.category).Distinct().ToList();
        }

        public List<object> FindItems()
        {
            return _hardwoodItems.Find(Builders<HardwoodItem>.Filter.Empty).ToList().Cast<object>().ToList();
        }
    }

    private class VinylItemFinder : IItemFinder
    {
         private readonly IMongoCollection<VinylItem> _vinylItems;

        public VinylItemFinder(IMongoDatabase database) 
        {
            _vinylItems = database.GetCollection<VinylItem>("VinylItems");
        }

        public List<string> FindCategories()
        {
            return _vinylItems.Find(Builders<VinylItem>.Filter.Empty).ToList().Select(item => item.category).Distinct().ToList();
        }

        public List<object> FindItems()
        {
            return _vinylItems.Find(Builders<VinylItem>.Filter.Empty).ToList().Cast<object>().ToList();
        }
    }

    private class LaminatedItemFinder : IItemFinder
    {
         private readonly IMongoCollection<LaminatedItem> _laminatedItems;

        public LaminatedItemFinder(IMongoDatabase database) 
        {
            _laminatedItems = database.GetCollection<LaminatedItem>("VinylItems");
        }

        public List<string> FindCategories()
        {
            return _laminatedItems.Find(Builders<LaminatedItem>.Filter.Empty).ToList().Select(item => item.category).Distinct().ToList();
        }

        public List<object> FindItems()
        {
            return _laminatedItems.Find(Builders<LaminatedItem>.Filter.Empty).ToList().Cast<object>().ToList();
        }
    }

    private class EngineeredItemFinder : IItemFinder
    {
         private readonly IMongoCollection<EngineeredItem> _engineeredItems;

        public EngineeredItemFinder(IMongoDatabase database) 
        {
            _engineeredItems = database.GetCollection<EngineeredItem>("EngineeredItems");
        }

        public List<string> FindCategories()
        {
            return _engineeredItems.Find(Builders<EngineeredItem>.Filter.Empty).ToList().Select(item => item.category).Distinct().ToList();
        }

        public List<object> FindItems()
        {
            return _engineeredItems.Find(Builders<EngineeredItem>.Filter.Empty).ToList().Cast<object>().ToList();
        }
    }

     private class MoldingItemFinder : IItemFinder
    {
         private readonly IMongoCollection<Molding> _moldingItems;

        public MoldingItemFinder(IMongoDatabase database) 
        {
            _moldingItems = database.GetCollection<Molding>("MoldingItems");
        }

        public List<string> FindCategories()
        {
            return _moldingItems.Find(Builders<Molding>.Filter.Empty).ToList().Select(item => item.category).Distinct().ToList();
        }

        public List<object> FindItems()
        {
            return _moldingItems.Find(Builders<Molding>.Filter.Empty).ToList().Cast<object>().ToList();
        }
    }


    
    private readonly Dictionary<string, IItemFinder> _itemFinders;
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Represents a controller for managing the catalog of items.
    /// </summary>
    public CatalogController(IMongoDatabase database)
    {
        _database = database;
        _itemFinders = new Dictionary<string, IItemFinder>()
        {
            { "Hardwood", new HardwoodItemFinder(database) },
            { "Vinyl", new VinylItemFinder(database) },
            { "Laminated", new LaminatedItemFinder(database) },
            { "Engineered", new EngineeredItemFinder(database) },
            { "Moulding", new MoldingItemFinder(database) }
        };
    }

    /// <summary>
    /// Retrieves the name of a catalog item based on its ID.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog item.</param>
    /// <returns>The name of the catalog item.</returns>
    private string _GetCatalogName(string catalogId) {
        var filter = Builders<CatalogItem>.Filter.Eq("id", catalogId);
        var catalog = _database.GetCollection<CatalogItem>("CatalogItems").Find(filter).FirstOrDefault();
        return catalog.name;
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
    public List<object> GetCatalogItems(string catalogId)
    {
        string catalogName = _GetCatalogName(catalogId);
        return _itemFinders[catalogName].FindItems();
    }

    /// <summary>
    /// Retrieves the categories for a given catalog.
    /// </summary>
    /// <param name="catalogId">The ID of the catalog.</param>
    /// <returns>A list of category names.</returns>
    public List<string> GetCatalogCategories(string catalogId)
    {
        string catalogName = _GetCatalogName(catalogId);
        return _itemFinders[catalogName].FindCategories();
    }
}
public class Catalog {
    public static List<string> catalog;

    public Catalog() {
        catalog = ["Moulding", "Vinyl", "Hardwood", "Laminated", "Engineered"];
    }

    public static List<string> GetCatalogs() {
        if (catalog == null) {
            new Catalog();
        }
        return catalog;
    }
}


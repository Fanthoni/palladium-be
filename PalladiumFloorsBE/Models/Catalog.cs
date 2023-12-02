// public enum Catalog {
//     Moulding="Moulding",
//     Vinyl="Vinyl",
//     Hardwood="Hardwood",
//     Laminated="Laminated",
//     Engineered="Engineered",
// }

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


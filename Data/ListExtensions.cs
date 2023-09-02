namespace CaptainsLog;

public static class ListExtensions {
    private static Random rng = new Random();

    public static T Random<T>(this List<T> items) {
        return items[rng.Next(items.Count)];
    }
}
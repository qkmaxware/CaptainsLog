namespace CaptainsLog.Data;

public static class Backgrounds {
    private static Random rng = new Random();

    public static string Random() {
        return All[rng.Next(All.Count)];
    }
    static public List<string> All = new List<string>{
        "assets/starfields/starfield_1.png",
        "assets/starfields/starfield_2.png",
        "assets/starfields/starfield_3.jpg",
        "assets/starfields/planet_1.jpg",
        "assets/starfields/planet_2.png",
    };
}
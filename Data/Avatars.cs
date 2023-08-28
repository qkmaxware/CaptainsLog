namespace CaptainsLog.Data;

public static class Avatars {
    private static Random rng = new Random();
    public static string RandomCommander() {
        return Commanders[rng.Next(Commanders.Count)];
    }
    static public List<string> Commanders = new List<string>{
        "assets/avatars/commander_1.png",
        "assets/avatars/commander_3.png",
    };
    static public List<string> All = new List<string>{
        "assets/avatars/commander_1.png",
        "assets/avatars/commander_3.png",
        "assets/avatars/operations_1.png",
        "assets/avatars/operations_2.png",
        "assets/avatars/science_1.png",
        "assets/avatars/science_2.png",
    };
}
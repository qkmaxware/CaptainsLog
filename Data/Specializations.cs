namespace CaptainsLog.Data;

public class Specialization {
    public string Name {get; set;}
    public string? Description {get; set;}
    public Specialization(string name, string? desc = null) {
        this.Name = name;
        this.Description = desc;
    }
}

public static class Specializations {
    private static Random rng = new Random();

    public static Specialization Random() {
        return All[rng.Next(All.Count)];
    }
    public static List<Specialization> All {get; private set;} = new List<Specialization> {
        new Specialization("Battlecruiser"),
        new Specialization("Civilian Merchant Marine"),
        new Specialization("Colony Support"),
        new Specialization("Crisis and Emergency Response"),
        new Specialization("Entertainment"),
        new Specialization("Intelligence"),
        new Specialization("Flagship"),
        new Specialization("Logistical"),
        new Specialization("Multirole Explorer"),
        new Specialization("Reconnaissance Operations"),
        new Specialization("Pathfinder Operations"),
        new Specialization("Patrol"),
        new Specialization("Reserve Fleet"),
        new Specialization("Scientific and Survey Operations"),
        new Specialization("Strategic and Diplomatic Operations"),
        new Specialization("Tactical Operations"),
        new Specialization("Technical Test-Bed"),
        new Specialization("Warship"),
    };
}
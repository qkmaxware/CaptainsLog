namespace CaptainsLog.Data;

public struct VesselAttributes {
    public int Comms {get; set;}
    public int Computers {get; set;}
    public int Engines {get; set;}
    public int Sensors {get; set;}
    public int Structure {get; set;}
    public int Weapons {get; set;}
}

public class Ship {
    public string? ImageUrl;
    public string? BackgroundImageUrl;
    public string? Name;
    public string? Registry;
    public string? Faction;
    public string? Specialization;
    public string? ClassName;
    public int Size;
    public List<Department>? Departments;

    public VesselAttributes Attributes;
    public Disciplines Disciplines;

    public List<string>? Traits;
    public List<string>? Features;

    private static Random rng = new Random();

    public static string GenRegistry() => "NCC " + rng.Next(10000);

    public static Ship Default() {
        return new Ship {
            // Note, I got a tonne of images from previous work here https://github.com/qkmaxware/TrekSharp/tree/root/TrekSharp.AdventureTools/wwwroot/assets/artwork/ships
            // May want to integrate those somehow with a class selector for easy use
            ImageUrl = "assets/default_vessel.png",
            Name = "USS Your Vessel",
            Registry = GenRegistry(),
            Specialization = "Exploratory Vessel",
            Size = 3,
            Departments = new List<Department>{
                Department.Default()
            }
        };
    }

    public override string ToString() {
        return $"{Name} {Registry}";
    }
}
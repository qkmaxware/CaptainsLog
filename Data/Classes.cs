namespace CaptainsLog.Data;

public class VesselClass {
    public string Name {get; set;}
    public int Scale {get; set;}
    public string? IconUrl {get; set;}
    public VesselClass(string name, int scale, string? iconUrl = null) {
        this.Name = name;
        this.Scale = scale;
        this.IconUrl = iconUrl;
    }
}

public static class VesselClasses {
    private static Random rng = new Random();

    public static VesselClass RandomStarfleet() {
        return Federation[rng.Next(Federation.Count)];
    }
    public static IEnumerable<VesselClass> All 
        => Federation
        .Concat(Klingon)
        .Concat(Romulan)
        .Concat(Dominion)
        .Concat(Ferengi)
        .Concat(Borg);
    public static List<VesselClass> Federation {get; private set;} = new List<VesselClass> {
        new VesselClass("Akira", 5, "assets/ships/ufp/AkiraClass.png"),
        new VesselClass("Ambassador", 5, "assets/ships/ufp/AmbassadorClass.png"),
        new VesselClass("California", 3, "assets/ships/ufp/CaliforniaClass.png"),
        new VesselClass("Centaur", 4, "assets/ships/ufp/CentaurClass.png"),
        new VesselClass("Challenger", 4, "assets/ships/ufp/ChallengerClass.png"),
        new VesselClass("Cheyenne", 4, "assets/ships/ufp/CheyenneClass.png"),
        new VesselClass("Constellation", 4, "assets/ships/ufp/ConstellationClass.png"),
        new VesselClass("Constitution", 4, "assets/ships/ufp/ConstitutionClass.png"),
        new VesselClass("Crossfield", 4, "assets/ships/ufp/CrossfieldClass.png"),
        new VesselClass("Daedalus", 3, "assets/ships/ufp/DaedalusClass.png"),
        new VesselClass("Defiant", 3, "assets/ships/ufp/DefiantClass.png"),
        new VesselClass("Excelsior", 5, "assets/ships/ufp/ExcelsiorClass.png"),
        new VesselClass("Freedom", 4, "assets/ships/ufp/FreedomClass.png"),
        new VesselClass("Galaxy", 6, "assets/ships/ufp/GalaxyClass.png"),
        new VesselClass("Hermes", 4, "assets/ships/ufp/HermesClass.png"),
        new VesselClass("Intrepid", 4, "assets/ships/ufp/IntrepidClass.png"),
        new VesselClass("Luna", 5, "assets/ships/ufp/LunaClass.png"),
        new VesselClass("Miranda", 4, "assets/ships/ufp/MirandaClass.png"),
        new VesselClass("Nebula", 5, "assets/ships/ufp/NebulaClass.png"),
        new VesselClass("New Orleans", 4, "assets/ships/ufp/NewOrleansClass.png"),
        new VesselClass("Niagara", 5, "assets/ships/ufp/NiagaraClass.png"),
        new VesselClass("Norway", 4, "assets/ships/ufp/NorwayClass.png"),
        new VesselClass("Nova", 3, "assets/ships/ufp/NovaClass.png"),
        new VesselClass("NX", 3, "assets/ships/ufp/NXClass.png"),
        new VesselClass("Oberth", 3, "assets/ships/ufp/OberthClass.png"),
        new VesselClass("Olympic", 4, "assets/ships/ufp/OlympicClass.png"),
        new VesselClass("Parliament", 3, "assets/ships/ufp/ParliamentClass.png"),
        new VesselClass("Sabre", 3, "assets/ships/ufp/SaberClass.png"),
        new VesselClass("Shepard", 3, "assets/ships/ufp/ShepardClass.png"),
        new VesselClass("Sovereign", 6, "assets/ships/ufp/SovereignClass.png"),
        new VesselClass("Springfield", 4, "assets/ships/ufp/SpringfieldClass.png"),
        new VesselClass("Steamrunner", 4, "assets/ships/ufp/SteamrunnerClass.png"),
        new VesselClass("Sydney", 4, "assets/ships/ufp/SydneyClass.png"),
        new VesselClass("Walker", 3, "assets/ships/ufp/WalkerClass.png"),
    };
    public static List<VesselClass> Klingon {get; private set;} = new List<VesselClass> {
        new VesselClass("B'Rel", 3, "assets/ships/klingon/B'RelClassBird-of-Prey.png"),
        new VesselClass("K'Vort", 3, "assets/ships/klingon/K'VortClassBird-of-Prey.png"),
        new VesselClass("Vor'Cha", 3, "assets/ships/klingon/Vor'ChaClassAttackCruiser.png"),
        new VesselClass("D7", 3, "assets/ships/klingon/D7BattleCruiser.png"),
    };
    public static List<VesselClass> Romulan {get; private set;} = new List<VesselClass> {
        new VesselClass("T'Liss", 3, "assets/ships/romulan/RomulanBird-of-Prey.png"),
        new VesselClass("D'Deridex", 3, "assets/ships/romulan/D'DeridexWarbird.png"),
    };
    public static List<VesselClass> Dominion {get; private set;} = new List<VesselClass> {
        new VesselClass("Jem'Hadar Attack Ship", 3, "assets/ships/dominion/Jem'HadarAttackShip.png"),
        new VesselClass("Jem'Hadar Battle Cruiser", 3, "assets/ships/dominion/Jem'HadarBattleCruiser.png"),
    };
    public static List<VesselClass> Ferengi {get; private set;} = new List<VesselClass> {
        new VesselClass("D'Kora", 3, "assets/ships/ferengi/D'KoraMarauder.png"),
    };
    public static List<VesselClass> Borg {get; private set;} = new List<VesselClass> {
        new VesselClass("Cube", 3, "assets/ships/borg/BorgCube.png"),
        new VesselClass("Sphere", 3, "assets/ships/borg/BorgSphere.png"),
    };

}
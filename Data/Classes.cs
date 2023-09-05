namespace CaptainsLog.Data;

public class VesselClass {
    public string Faction {get; set;}
    public string Name {get; set;}
    public int Scale {get; set;}
    public VesselClassArtwork Icons {get; set;}
    public VesselClass(string faction, string name, int scale, string? iconUrl = null) {
        this.Faction = faction;
        this.Name = name;
        this.Scale = scale;
        this.Icons = new VesselClassArtwork {
            SilhouetteIconUrl = iconUrl
        };
    }
    public VesselClass(string faction, string name, int scale, VesselClassArtwork icons) {
        this.Faction = faction;
        this.Name = name;
        this.Scale = scale;
        this.Icons = icons;
    }
}

public class VesselClassArtwork {
    public string? SilhouetteIconUrl {get; set;}
    public string? PhysicalModelIconUrl {get; set;}
    public string? RenderedIconUrl {get; set;}

    public string? GetPreferredArtwork() {
        return EnumerateArtwork().FirstOrDefault();
    }
    public IEnumerable<string> EnumerateArtwork() {
        if (!string.IsNullOrEmpty(this.RenderedIconUrl))
            yield return this.RenderedIconUrl;

        if (!string.IsNullOrEmpty(this.PhysicalModelIconUrl))
            yield return this.PhysicalModelIconUrl;

        if (!string.IsNullOrEmpty(this.SilhouetteIconUrl))
            yield return this.SilhouetteIconUrl;
    }
}

public static class VesselClasses {
    private static Random rng = new Random();

    /// <summary>
    /// Pick a random starfleet vessel class
    /// </summary>
    /// <returns>class</returns>
    public static VesselClass RandomStarfleet() {
        return Federation[rng.Next(Federation.Count)];
    }

    /// <summary>
    /// List of all vessels
    /// </summary>
    public static IEnumerable<VesselClass> All 
        => Federation
        .Concat(Klingon)
        .Concat(Romulan)
        .Concat(Dominion)
        .Concat(Cardassian)
        .Concat(Ferengi)
        .Concat(Borg);

    /// <summary>
    /// List of UFP vessels
    /// </summary>
    public static List<VesselClass> Federation {get; private set;} = new List<VesselClass> {
        new VesselClass("Starfleet", "Akira", 5, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/AkiraClass.png",
            PhysicalModelIconUrl = "assets/ships/ufp/AkiraClass.WizKidz.png"
        }),
        new VesselClass("Starfleet", "Ambassador", 5, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/AmbassadorClass.png",
            RenderedIconUrl = "assets/ships/ufp/AmbassadorClass.Render.png",
        }),
        new VesselClass("Starfleet", "California", 3, "assets/ships/ufp/CaliforniaClass.png"),
        new VesselClass("Starfleet", "Centaur", 4, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/CentaurClass.png",
            RenderedIconUrl = "assets/ships/ufp/CentaurClass.Render.png",
        }),
        new VesselClass("Starfleet", "Challenger", 4, "assets/ships/ufp/ChallengerClass.png"),
        new VesselClass("Starfleet", "Cheyenne", 4, "assets/ships/ufp/CheyenneClass.png"),
        new VesselClass("Starfleet", "Constellation", 4, "assets/ships/ufp/ConstellationClass.png"),
        new VesselClass("Starfleet", "Constitution", 4, "assets/ships/ufp/ConstitutionClass.png"),
        new VesselClass("Starfleet", "Crossfield", 4, "assets/ships/ufp/CrossfieldClass.png"),
        new VesselClass("Starfleet", "Daedalus", 3, "assets/ships/ufp/DaedalusClass.png"),
        new VesselClass("Starfleet", "Defiant", 3, "assets/ships/ufp/DefiantClass.png"),
        new VesselClass("Starfleet", "Excelsior", 5, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/ExcelsiorClass.png",
            PhysicalModelIconUrl = "assets/ships/ufp/ExcelsiorClass.WizKidz.png",
            RenderedIconUrl = "assets/ships/ufp/ExcelsiorClass.Render.png",
        }),
        new VesselClass("Starfleet", "Freedom", 4, "assets/ships/ufp/FreedomClass.png"),
        new VesselClass("Starfleet", "Galaxy", 6, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/GalaxyClass.png",
            PhysicalModelIconUrl = "assets/ships/ufp/GalaxyClass.WizKidz.png",
            RenderedIconUrl = "assets/ships/ufp/GalaxyClass.Render.png",
        }),
        new VesselClass("Starfleet", "Hermes", 4, "assets/ships/ufp/HermesClass.png"),
        new VesselClass("Starfleet", "Intrepid", 4, "assets/ships/ufp/IntrepidClass.png"),
        new VesselClass("Starfleet", "Luna", 5, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/LunaClass.png",
            RenderedIconUrl = "assets/ships/ufp/LunaClass.Render.png",
        }),
        new VesselClass("Starfleet", "Miranda", 4, "assets/ships/ufp/MirandaClass.png"),
        new VesselClass("Starfleet", "Nebula", 5, new VesselClassArtwork{
            SilhouetteIconUrl = "assets/ships/ufp/NebulaClass.png",
            PhysicalModelIconUrl = "assets/ships/ufp/NebulaClass.WizKidz.png"
        }),
        new VesselClass("Starfleet", "New Orleans", 4, "assets/ships/ufp/NewOrleansClass.png"),
        new VesselClass("Starfleet", "Niagara", 5, "assets/ships/ufp/NiagaraClass.png"),
        new VesselClass("Starfleet", "Norway", 4, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/NorwayClass.png",
            RenderedIconUrl = "assets/ships/ufp/NorwayClass.Render.png",
        }),
        new VesselClass("Starfleet", "Nova", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/NovaClass.png",
            RenderedIconUrl = "assets/ships/ufp/NovaClass.Render.png",
        }),
        new VesselClass("Starfleet", "NX", 3, "assets/ships/ufp/NXClass.png"),
        new VesselClass("Starfleet", "Oberth", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/OberthClass.png",
            RenderedIconUrl = "assets/ships/ufp/OberthClass.Render.png",
        }),
        new VesselClass("Starfleet", "Olympic", 4, "assets/ships/ufp/OlympicClass.png"),
        new VesselClass("Starfleet", "Parliament", 3, "assets/ships/ufp/ParliamentClass.png"),
        new VesselClass("Starfleet", "Prometheus", 4, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/PrometheusClass.png",
            RenderedIconUrl = "assets/ships/ufp/PrometheusClass.Render.png",
        }),
        new VesselClass("Starfleet", "Sabre", 3, "assets/ships/ufp/SaberClass.png"),
        new VesselClass("Starfleet", "Shepard", 3, "assets/ships/ufp/ShepardClass.png"),
        new VesselClass("Starfleet", "Sovereign", 6, "assets/ships/ufp/SovereignClass.png"),
        new VesselClass("Starfleet", "Springfield", 4, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ufp/SpringfieldClass.png",
            RenderedIconUrl = "assets/ships/ufp/SpringfieldClass.Render.png",
        }),
        new VesselClass("Starfleet", "Steamrunner", 4, "assets/ships/ufp/SteamrunnerClass.png"),
        new VesselClass("Starfleet", "Sydney", 4, "assets/ships/ufp/SydneyClass.png"),
        new VesselClass("Starfleet", "Walker", 3, "assets/ships/ufp/WalkerClass.png"),
    };
    /// <summary>
    /// List of Klingon Vessels
    /// </summary>
    public static List<VesselClass> Klingon {get; private set;} = new List<VesselClass> {
        new VesselClass("Klingon Empire", "B'Rel", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/klingon/B'RelClassBird-of-Prey.png",
            PhysicalModelIconUrl = "assets/ships/klingon/B'RelClassBird-of-Prey.WizKidz.png",
            RenderedIconUrl = "assets/ships/klingon/B'RelClassBird-of-Prey.Render.png",
        }),
        new VesselClass("Klingon Empire", "K'Vort", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/klingon/K'VortClassBird-of-Prey.png",
            //RenderedIconUrl = "assets/ships/klingon/B'RelClassBird-of-Prey.Render.png",
        }),
        new VesselClass("Klingon Empire", "Vor'Cha", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/klingon/Vor'ChaClassAttackCruiser.png",
            PhysicalModelIconUrl = "assets/ships/klingon/Vor'ChaClassAttackCruiser.WizKidz.png",
            RenderedIconUrl = "assets/ships/klingon/Vor'ChaClassAttackCruiser.Render.png",
        }),
        new VesselClass("Klingon Empire", "K'Tinga", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/klingon/D7BattleCruiser.png",
            RenderedIconUrl = "assets/ships/klingon/K'Tinga.Render.png",
        }),
    };
    /// <summary>
    /// List of Romulan Vessels
    /// </summary>
    public static List<VesselClass> Romulan {get; private set;} = new List<VesselClass> {
        new VesselClass("Romulan Star Empire", "T'Liss", 3, "assets/ships/romulan/RomulanBird-of-Prey.png"),
        new VesselClass("Romulan Star Empire", "D'Deridex", 3, "assets/ships/romulan/D'DeridexWarbird.png"),
    };
    /// <summary>
    /// List of Dominion Vessels
    /// </summary>
    public static List<VesselClass> Dominion {get; private set;} = new List<VesselClass> {
        new VesselClass("The Dominion", "Jem'Hadar Attack Ship", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/dominion/Jem'HadarAttackShip.png",
            PhysicalModelIconUrl = "assets/ships/dominion/Jem'HadarAttackShip.WizKidz.png",
        }),
        new VesselClass("The Dominion", "Jem'Hadar Battle Cruiser", 3, "assets/ships/dominion/Jem'HadarBattleCruiser.png"),
    };
    /// <summary>
    /// List of Cardassian Vessels
    /// </summary>
    public static List<VesselClass> Cardassian {get; private set;} = new List<VesselClass> {
        new VesselClass("Cardassian Union", "Galor", 3, "assets/ships/cardassian/GalorClass.png"),
    };
    /// <summary>
    /// List of Ferengi Vessels
    /// </summary>
    public static List<VesselClass> Ferengi {get; private set;} = new List<VesselClass> {
        new VesselClass("Ferengi Alliance", "D'Kora", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/ferengi/D'KoraMarauder.png",
            RenderedIconUrl = "assets/ships/ferengi/D'KoraMarauder.Render.png",
        }),
    };
    /// <summary>
    /// List of Borg Vessels
    /// </summary>
    public static List<VesselClass> Borg {get; private set;} = new List<VesselClass> {
        new VesselClass("Borg Collective", "Cube", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/borg/BorgCube.png",
            RenderedIconUrl = "assets/ships/borg/BorgCube.Render.png",
        }),
        new VesselClass("Borg Collective", "Sphere", 3, new VesselClassArtwork {
            SilhouetteIconUrl = "assets/ships/borg/BorgSphere.png",
            RenderedIconUrl = "assets/ships/borg/BorgSphere.Render.png",
        }),
    };

}
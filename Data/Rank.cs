using System.Reflection;

namespace CaptainsLog.Data;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class RankMetaAttribute : System.Attribute {
    public string Name {get; private set;}
    public RankMetaAttribute(string name) {
        this.Name = name;
    }
}

public enum Rank {
    [RankMeta("Crewman")]
    Crewman, 
    [RankMeta("Petty Officer")]
    PettyOfficer,
    [RankMeta("Chief Petty Officer")]
    ChiefPettyOfficer,
    [RankMeta("Senior Petty Officer")]
    SeniorPettyOfficer,
    [RankMeta("Master Chief Petty Officer")]
    MasterChiefPettyOfficer,
    [RankMeta("Ensign")]
    Ensign,
    [RankMeta("Lieutenant Junior Grade")]
    LieutenantJuniorGrade,
    [RankMeta("Lieutenant")]
    Lieutenant,
    [RankMeta("Lieutenant Commander")]
    LieutenantCommander,
    [RankMeta("Commander")]
    Commander,
    [RankMeta("Captain")]
    Captain,
    [RankMeta("Fleet Captain")]
    FleetCaptain,
    [RankMeta("Commodore")]
    Commodore,
    [RankMeta("Rear Admiral")]
    RearAdmiral,
    [RankMeta("Vice Admiral")]
    ViceAdmiral,
    [RankMeta("Admiral")]
    Admiral,
    [RankMeta("Fleet Admiral")]
    FleetAdmiral,
    [RankMeta("United Federation of Planets President")]
    UfpPresident
}

public static class RankExtensions {
    public static string GetName(this Rank value) {
        FieldInfo? fi = value.GetType().GetField(value.ToString());
        RankMetaAttribute[]? attributes = (RankMetaAttribute[]?)fi?.GetCustomAttributes(typeof(RankMetaAttribute), false);
        if (!ReferenceEquals(attributes, null) && attributes.Length > 0)
            return attributes[0].Name;
        else
            return value.ToString();
    }
}
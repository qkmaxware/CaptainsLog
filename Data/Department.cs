namespace CaptainsLog.Data;

public class Department {
    public string? Name;
    public List<Character>? Characters;

    public static Department Default() {
        return new Department{
            Name = "Bridge Officers",
            Characters = new List<Character> {
            }
        };
    }
}
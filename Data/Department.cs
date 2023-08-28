namespace CaptainsLog.Data;

public class Department {
    public string? Name;
    public List<Character>? Characters;

    public static Department Default() {
        return new Department{
            Name = "Bridge Officers",
            Characters = new List<Character> {
                new Character {
                    ImageUrl = Avatars.RandomCommander(),
                    Rank = Rank.Captain,
                    Division = Division.Command,
                    Name = "Your Character",
                    Assignment = "Commanding Officer",
                    Focuses = new List<string>()
                }
            }
        };
    }
}
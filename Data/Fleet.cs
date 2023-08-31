using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CaptainsLog.Data;

public class Fleet {
    public List<Ship>? Ships;

    [JsonIgnore]
    public IEnumerable<Character> Characters {
        get {
            if (ReferenceEquals(Ships, null))
                yield break;
            foreach (var ship in Ships) {
                if (ReferenceEquals (ship, null) || ReferenceEquals(ship.Departments, null)) {
                    continue;
                }

                foreach (var dept in ship.Departments) {
                    if (ReferenceEquals(dept.Characters, null)) {
                        continue;
                    }

                    foreach (var character in dept.Characters) {
                        if (ReferenceEquals(character, null)) {
                            continue;
                        }

                        yield return character;
                    }
                }
            }
        }
    }
}
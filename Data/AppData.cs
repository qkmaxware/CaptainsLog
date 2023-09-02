using System.Diagnostics.CodeAnalysis;
using CaptainsLog.Data;

namespace CaptainsLog;

public interface IAppDataProvider {
    public AppData GetAppData();
    public void LoadAppData(AppData data);
    public void Save();
}

public struct Missions {
    public MissionBriefing? Current;
    public List<MissionBriefing>? Completed;
}

public class AppData {
    public Fleet? Fleet;
    public PersonalLog? Log;
    public Missions Missions;

    public IEnumerable<Ship> EnumerateVessels() {
        if (ReferenceEquals(Fleet, null) || ReferenceEquals(Fleet.Ships, null)) {
            return Enumerable.Empty<Ship>();
        }
        return Fleet.Ships;
    }

    public IEnumerable<Character> EnumerateCharacters() {
        if (ReferenceEquals(Fleet, null)) {
            return Enumerable.Empty<Character>();
        }
        return Fleet.Characters;
    }
    
    public Ship? FindShipWithRegistryNumber(string? number) {
        if (ReferenceEquals(Fleet, null) || ReferenceEquals(Fleet.Ships, null)) {
            return null;
        }
        if (string.IsNullOrEmpty(number)) {
            return null;
        }

        foreach (var ship in Fleet.Ships) {
            if (number == ship.Registry) {
                return ship;
            }
        }
        return null;
    }

    public Ship? FindAssignedVessel(Character character) {
        if (ReferenceEquals(Fleet, null) || ReferenceEquals(Fleet.Ships, null)) {
            return null;
        }

        foreach (var ship in Fleet.Ships) {
            if (ReferenceEquals (ship, null) || ReferenceEquals(ship.Departments, null)) {
                continue;
            }

            foreach (var dept in ship.Departments) {
                if (ReferenceEquals(dept.Characters, null)) {
                    continue;
                }

                if (dept.Characters.Contains(character)) {
                    return ship;
                }
            }
        }

        return null;
    }

    private static Random rng = new Random();
    public bool FindFavoriteCharacter([NotNullWhen(true)] out Ship? ship, [NotNullWhen(true)] out Department? department, [NotNullWhen(true)] out Character? character) {
        ship = null;
        department = null;
        character = null;

        if (ReferenceEquals(Fleet, null) || ReferenceEquals(Fleet.Ships, null)) {
            return false;
        }

        var favs = new List<(Ship, Department, Character)>();

        foreach (var vship in Fleet.Ships) {
            if (ReferenceEquals (vship, null) || ReferenceEquals(vship.Departments, null)) {
                continue;
            }

            foreach (var vdept in vship.Departments) {
                if (ReferenceEquals(vdept.Characters, null)) {
                    continue;
                }

                foreach (var c in vdept.Characters) {
                    if (c.IsFavorite) {
                        favs.Add((vship, vdept, c));
                    }
                }
            }
        }

        if (favs.Count < 1)
            return false;

        var x = favs[rng.Next(favs.Count)];
        ship = x.Item1;
        department = x.Item2;
        character = x.Item3;
        return true;
    }
}
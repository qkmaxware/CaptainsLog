namespace CaptainsLog;

public class RandomPick<T> {
    private List<T> options;
    public RandomPick(List<T> options) {
        this.options = options;
    }
    public virtual IEnumerable<T> GetRandom() {
        yield return options.Random();
    }
}

public class RandomPick2<T> : RandomPick<T> {
    private List<RandomPick<T>> options;
    public RandomPick2(List<RandomPick<T>> options) : base(new List<T>()) {
        this.options = options;
    }
    public override IEnumerable<T> GetRandom() {
        foreach (var o in options.Random().GetRandom()){
            yield return o;
        }
        foreach (var o in options.Random().GetRandom()){
            yield return o;
        }
    }
}

public class RandomPick2String : RandomPick<string> {
    private List<RandomPick<string>> src;
    public RandomPick2String(List<RandomPick<string>> options) : base(new List<string>()) {
        this.src = options;
    }
    public override IEnumerable<string> GetRandom() {
        var first = src.Random();
        var second = src.Random();
        yield return string.Join(" & ", first.GetRandom()) + " & " + string.Join(" & ", second.GetRandom());
    }
}

public class MissionType {
    public string Name {get; private set;}
    protected List<string> subcategories = new List<string>();

    public MissionType(string name, List<string> subcategories) {
        this.Name = name;
        this.subcategories = subcategories;
    }

    public virtual MissionBriefing CreateBriefing() {
        return new MissionBriefing {
            Type = this.Name,
            Categories = new string[]{ subcategories.Random() }, 
        };
    }
}

public class MultiMissionType : MissionType {
    private List<MissionType> types;
    public MultiMissionType(List<MissionType> types) : base("Multi-type", new List<string>()) {
        this.types = types;
    }
    public override MissionBriefing CreateBriefing() {
        var first = types.Random();
        var bf1 = first.CreateBriefing();
        var second = types.Random();
        var bf2 = second.CreateBriefing();
        return new MissionBriefing {
            Type = first.Name + " & " + second.Name,
            Categories = (bf1.Categories ?? new string[0]).Concat(bf2.Categories ?? new string[0]).ToArray(), 
        };
    }
}

public class Advantage {
    public string? Name;
    public string? Description;

    public Advantage() {}
    public Advantage(string name, string? description = null) {
        this.Name = name;
        this.Description = description;
    }
}

public class Complication {
    public string? Name;
    public string? Description;
    public Complication() {}
    public Complication(string name, string? description = null) {
        this.Name = name;
        this.Description = description;
    }
}

public enum MissionSceneState {
    NotDone = 0,
    CompleteNotWell = 1,
    CompleteWell = 2
}

public enum MissionScore {
    C, B, A, S
}

public struct MissionProgress {
    public MissionSceneState Scene1;
    public MissionSceneState Scene2;
    public MissionSceneState Scene3;
    public MissionSceneState Scene4;
    public MissionSceneState Scene5;
    public MissionSceneState Scene6;
    public MissionSceneState Scene7;
    public MissionSceneState Scene8;
    public MissionSceneState Scene9;
    public MissionSceneState Scene10;
    public MissionSceneState Scene11;
    public MissionSceneState Scene12;
    public MissionSceneState Scene13;
    public MissionSceneState Scene14;
    public MissionSceneState Scene15;

    public MissionScore Grade() {
        int completed = 0;
        int score = 0;
        
        if (Scene1 != MissionSceneState.NotDone) { completed++; score+=(int)Scene1; }
        if (Scene2 != MissionSceneState.NotDone) { completed++; score+=(int)Scene2; }
        if (Scene3 != MissionSceneState.NotDone) { completed++; score+=(int)Scene3; }
        if (Scene4 != MissionSceneState.NotDone) { completed++; score+=(int)Scene4; }
        if (Scene5 != MissionSceneState.NotDone) { completed++; score+=(int)Scene5; }
        if (Scene6 != MissionSceneState.NotDone) { completed++; score+=(int)Scene6; }
        if (Scene7 != MissionSceneState.NotDone) { completed++; score+=(int)Scene7; }
        if (Scene8 != MissionSceneState.NotDone) { completed++; score+=(int)Scene8; }
        if (Scene9 != MissionSceneState.NotDone) { completed++; score+=(int)Scene9; }
        if (Scene10 != MissionSceneState.NotDone) { completed++; score+=(int)Scene10; }
        if (Scene11 != MissionSceneState.NotDone) { completed++; score+=(int)Scene11; }
        if (Scene12 != MissionSceneState.NotDone) { completed++; score+=(int)Scene12; }
        if (Scene13 != MissionSceneState.NotDone) { completed++; score+=(int)Scene13; }
        if (Scene14 != MissionSceneState.NotDone) { completed++; score+=(int)Scene14; }
        if (Scene15 != MissionSceneState.NotDone) { completed++; score+=(int)Scene15; }

        var percent = ((float)score / ((float)completed * (int)MissionSceneState.CompleteWell));

        return percent switch {
            _ when percent < 0.6f                       => MissionScore.C,
            _ when percent >= 0.6f && percent <= 0.8f   => MissionScore.B,
            _ when percent > 0.8f && percent < 1f       => MissionScore.A,
            _ when percent >= 1f                        => MissionScore.S,
            _                                           => MissionScore.C
        };
    }
}

public class MissionBriefing {
    public DateTime Started;
    public DateTime Completed;
    public string? Type;
    public string[]? Categories;
    public string? IncitingIncident;
    public Advantage[]? Advantages;
    public Complication[]? Complications;

    public MissionProgress Progress;
}

/*
2a. Select mission type from the table on 257
2b. roll on the category matrix

3. Generate incident theme advantages and complications
page 267

4. Roll an encounter page 270

5. Generate people, places, and things

6. Write your log example on page 234
*/

public class MissionBriefingGenerator {
    private List<MissionType> missionTypes = new List<MissionType>();
    private List<RandomPick<string>> incidents = new List<RandomPick<string>>();
    private List<RandomPick<string>> themes = new List<RandomPick<string>>();

    private List<RandomPick<Advantage>> advantages = new List<RandomPick<Advantage>>();
    private List<RandomPick<Complication>> complications = new List<RandomPick<Complication>>();

    public MissionBriefingGenerator() {
        #region Mission Types
        List<MissionType> singleTypes = new List<MissionType>();
        singleTypes.Add(new MissionType("Aid and Relief", new List<string> {
            "Discover stowaway on another ship",
            "Ensure key data is transmitted",
            "Ferry food to famine-stricken world",
            "Find lost escape pod",
            "Fulfill dying officer's last wish",
            "Help build back a failed economy",
            "Improve sensors to detect hostiles",
            "Investigate why convoy is under attack",
            "Locate ship lost in anomaly",
            "Negotiate trade agreement to save species",
            "Plot course for convoy",
            "Prevent natural disaster with science",
            "Protect merchant convoy",
            "Provide aid to an enemy",
            "Provide counseling services to distressed parties",
            "Provide needed resource to station or ship",
            "Repair colony's defense grid",
            "Respond to planetary distress call",
            "Upgrade civilian ships"
        }));
        singleTypes.Add(new MissionType("Conspiracy", new List<string> {
            "Allied with the enemy",
            "Assassination planned",
            "Conspiracy theory gone wild",
            "Corrupt Starfleet offical",
            "Crewmate lying",
            "Faked death",
            "Government undermined from within",
            "Informant discloses secret",
            "Intelligence leak",
            "Intercepted messages",
            "Misleading proaganda",
            "New tech is spyware",
            "Official bribed",
            "Paranoid pre-warp species",
            "Politician poisoned or attacked",
            "Possessed by alien entity",
            "Replaced by body double",
            "Sabotaged negotiations",
            "Smuggling contraband",
            "Unauthorized surveillance"
        }));
        singleTypes.Add(new MissionType("Deep Space Exploration", new List<string> {
            "Abandoned world or vessel",
            "Advisor accompanying mission has a mission of their own",
            "Alien explorers were there before",
            "Cosmozoan life-form is hungry",
            "Cozmozoan life-form mistakes ship for rival",
            "Cosmozoan life-form wants to mate with vessel",
            "Cryostasis is required to survive",
            "Detect incoming threat",
            "Discover a planet made of a strange substance",
            "Dropping subspace beacons",
            "Mapping a sector of space",
            "Nebula is a sentient being",
            "No communication with home",
            "Peaceful planet offers respite",
            "Ship becomes out of phase with reality",
            "Strange probe discovered",
            "Struck by object that sends ship off course",
            "Test a technology",
            "Warp does not work",
            "Wormhole of unknown origins suddenly appears"
        }));
        singleTypes.Add(new MissionType("Defense", new List<string> {
            "Argue the benefits of the prime directive",
            "Defend colony",
            "Defend ship from sentient entity",
            "Defend space station",
            "Defend the Federation from a smear campaign",
            "Engage in war games with a tactical advisor",
            "Join fleet for defensive maneuvers",
            "Neutral Zone violation",
            "Retrieve lost weapons",
            "Protect the rights of a religious group",
            "Provide protection for diplomat",
            "Represent a foreign polity as a neutral party",
            "Secure trade location",
            "Security services for vital meeting",
            "Set up defense for faction",
            "Ship's tactical system taken over by hostile",
            "Tactical maneuvers with ally",
            "Uphold the rights of a Federation citizen",
            "Uphold the rights of Federation world",
            "Uphold the laws of a non-Federation world"
        }));
        singleTypes.Add(new MissionType("Diplomacy", new List<string> {
            "Attend official event",
            "Conduct anti-propaganda campaign",
            "Draft peace treaty",
            "Establish trade routes",
            "Facilitate government restructure",
            "Host cultural exchange",
            "Implement new policy",
            "Implement training program",
            "Investigate civil unrest",
            "Mediate between rival factions",
            "Negotiate release of political prisoner",
            "Negotiate surrender",
            "Negotiate trade deal",
            "Serve as advisory committee",
            "Set up Starfleet Academy extension",
            "Settle border conflict",
            "Settle international dispute",
            "Settle interplanetary dispute",
            "Sign mining accord",
            "Oversee election"
        }));
        singleTypes.Add(new MissionType("Escort and Evacuation", new List<string> {
            "Aid scientists trapped in another realm",
            "Attend congratulatory celebration for successful mission",
            "Command fleet operations during evacuation",
            "Deliver command officers to their new ship",
            "Escort admiral to retirement location",
            "Escort dignitary to crucial meeting",
            "Escort key diplomat(s)",
            "Evacuate planet",
            "Evacuate ship about to explode",
            "Evacuate space station",
            "Evacuee out for revenge",
            "Help evacuation efforts before natural disaster",
            "Mechanical failure on escorted ship",
            "Plot course for convoy",
            "Rescue miners",
            "Retrieve tactical team",
            "Save endangered species from dying world",
            "Shepard civilian transport",
            "Strategize evacuation scenario",
            "Transfer supplies from one planet to another"
        }));
        singleTypes.Add(new MissionType("Espionage", new List<string> {
            "Analyze captured prisoners",
            "Analyze data to uncover plot",
            "Collect data",
            "Cosmetic surgery required",
            "Decrypt message",
            "Establish underworld contact",
            "Faction sabotages negotiations",
            "Fall off the grid to perform mission",
            "Find weapons cache",
            "Forensics investigation tainted",
            "Infiltrate criminal gang or terrorist organization",
            "Intercept comm chatter",
            "Monitor ship movements",
            "Present evidence of conspiracy",
            "Psychographic debriefing required",
            "Psyops required",
            "Recover hostages from unfriendly territory",
            "Section 31 assignment",
            "Spies discovered",
            "Undercover operation into enemy territory"
        }));
        singleTypes.Add(new MissionType("First Contact", new List<string> {
            "Ascertain biochemical compatibility",
            "Assemble first contact team",
            "Catalog diseases and pathogens",
            "Catalog new technology",
            "Compile linguistic database",
            "Complete computer network integration",
            "Create psychological profile of new species",
            "Don ceremonial garb",
            "Eat strange food",
            "Establish security protocols",
            "Establish transport procedures",
            "Fix first contact gone wrong",
            "Host diplomatic reception",
            "Learn native dance",
            "Make first contact",
            "Participate in odd welcoming ceremony",
            "Prepare xenoanthropological report",
            "Repair malfunctioning duck blind",
            "Second contact mission",
            // TODO "Roll secondary directive from any mission type matrix"
        }));
        singleTypes.Add(new MissionType("Medical", new List<string> {
            "All crew get sick",
            "Asked by enemies to assist with research",
            "Bio-weapon unleashed",
            "Conduct medical forensics investigation",
            "Conduct routine cre evaluations",
            "Crew catches new flu bug",
            "Crew sick after strange event",
            "Crew transformed into something else",
            "Crew members going insane",
            "Dead person miraculously comes back to life",
            "Disease is solution",
            "Doctor abducted to help terrorist group",
            "Medical supplies tainted",
            "Plague on alien ship or station",
            "Plague on enemy ship or station",
            "Plague on planet",
            "Psychic disease spreads",
            "Psychographic profile needed of species",
            "Ship system causes sickness",
            "Speak at medical conference"
        }));
        singleTypes.Add(new MissionType("Near Space Exploration", new List<string> {
            "Alien prove enters Federation space",
            "Art competition/show off your talents",
            "Collect data on spatial anomaly",
            "Crew member becomes a celebrity",
            "Dedicate new colony",
            "Dedicate new ship",
            "Eliminate dangerous spatial phenomena",
            "Enemy faction has established peaceful colony",
            "Enemy polity makes a power garb",
            "Family comes to visit",
            "Host meeting with admirals",
            "Lost starbase",
            "New and different Bajoran orb discovered",
            "New mineral or energy resource under dispute",
            "New planet appears in Federation space",
            "Seek permission from Starfleet to conduct fringe research",
            "Sensors detect hidden foe",
            "Sojourn back to homeworld",
            "Treaty violations must be investigated",
            "Visit local pleasure planet"
        }));
        singleTypes.Add(new MissionType("Patrol", new List<string> {
            "Analyze sensor readings or unknown ship",
            "Away team patrols colony",
            "Enemy blockade",
            "Establish new patrol route",
            "Find lost vessel",
            "Install new sensor array",
            "Investigate enemy fleet movements",
            "Investigate strange event or anomaly",
            "Long patrol, holodeck time",
            "Monitor comm traffic",
            "Negotiate patrol route",
            "New warp-capable civilization contacted",
            "Patrol hot-spot",
            "Repair collapsed defense grid",
            "Rogue planet drifts into trade route",
            "Search and rescue mission",
            "Search detained vessel",
            "Space mines inhibit movement",
            "Survey a planetary surface",
            "Tech floating adrift in space"
        }));
        singleTypes.Add(new MissionType("Plant Exploration", new List<string> {
            "Cetacean planet",
            "Corrosive environment",
            "Crashed ship",
            "Dry seabed reveals ancient civilization",
            "Ecological collapse",
            "Enemy research facility",
            "Forbidden world",
            "Geothermal springs offer health benefits",
            "Holy planet",
            "Mass graves",
            "Massive battlefield",
            "Meteorites constantly entering atmosphere",
            "Pre-warp species detects starship",
            "Prison planet",
            "Radioactive fog hides secret colony",
            "Rare element discovered",
            "Secret weapons facility",
            "Sentient liquid metal pools",
            "Trap",
            "Unusual flora or fauna holds solution"
        }));
        singleTypes.Add(new MissionType("Political", new List<string> {
            "Analyze conflict",
            "Application to join Federation",
            "Dismantle puppet government",
            "Dissolve monetary system",
            "Enforce Federation law",
            "Expose corrupt politician",
            "Extradite prisoner",
            "Guard duty for rude dignitary",
            "Honor retiring politician",
            "Investigate use of banned technology",
            "Investigate voter fraud",
            "Issue arrest warrant",
            "Manipulated media",
            "Negotiate cease-fire",
            "Politician needs a favor",
            "Sanction noncompliant Federation planet",
            "Stop flow of biogenic weaponry",
            "Succession from the Federation",
            "Transport cultural delegation",
            "Violent uprising disrupts peace talks"
        }));
        singleTypes.Add(new MissionType("Research and Development", new List<string> {
            "Astrometrics enhancement",
            "Communications upgrade",
            "Deflector upgrade",
            "Engine upgrade",
            "Exploring the mind",
            "Holographic technology",
            "Innovation with replicators",
            "Inter-dimensional event",
            "Medical innovation",
            "Navigational system",
            "New energy resource",
            "New polymer",
            "Power systems upgrade",
            "Prime Directive's relevance questioned",
            "Sensors upgrade",
            "Temporal sciences",
            "Terraforming technology",
            "Transporter technology",
            "Utopia Planitia project",
            "Weapons development"
        }));
        singleTypes.Add(new MissionType("Show the Flag", new List<string> {
            "Assist in building terraforming facilities",
            "Construct water system on planet",
            "Dedicate new space station",
            "Deliver supplies to colony along Neutral Zone",
            "Develop a new strain of disease-resistant food",
            "Help alien species navigate safe trade route",
            "Help rival cure disease",
            "Lay claim to a planet",
            "Plant the flag on a moon",
            "Provide guidance on terraforming",
            "Reinforce collapsed mineshaft",
            "Repair malfunctioning weather control system",
            "Secure asteroid for mining",
            "Set up schools",
            "Set up social program after a disaster",
            "Stop an asteroid from hitting planet",
            "Stop hostiles from harassing colony",
            "Track down pirates",
            "Transport staff to relay station",
            "Visit new member world"
        }));
        singleTypes.Add(new MissionType("Spiritual", new List<string> {
            "Ancient deity appears",
            "Archeological dig uncovers hidden truth",
            "Assist with restoration of religious site",
            "Community has decided to give up religion",
            "Crew member has a prophetic vision",
            "Deities are proven to be other-dimensional aliens",
            "Entire population has shared vision",
            "Exiled spiritual figure returns",
            "Life after death experience",
            "Minority group wants representation",
            "New religion begins to form",
            "Prophesied disaster occurs",
            "Religious dissidents exiled",
            "Religious minority takes extreme action",
            "Religious sect forbids alien visitors",
            "Science conflicts with long-held belief",
            "Spiritual figure assassinated",
            "Temple defaced or destroyed",
            "Tradition unwittingly defiled by crew",
            "Vow broken by holy order"
        }));
        singleTypes.Add(new MissionType("Starfleet JAG", new List<string> {
            "Advise Starfleet on enemy species",
            "Advise Starfleet on territory boundaries",
            "Argue case on behalf of former admiral",
            "Conduct forensics investigation at crime scene",
            "Defend fellow crew member in alien court",
            "Defend Starfleet officer in court",
            "Discipline rowdy cadets",
            "Investigate corrupt judge",
            "Investigate destruction of relay station",
            "Investigate wrongful death",
            "Negotiate land settlement",
            "Prosecute contraband case",
            "Prosecute environmental crime",
            "Prosecute errant captain",
            "Prosecute war crime",
            "Review rules of engagement",
            "Serve on court martial case",
            "Serve subpoena to reclusive Starfleet individual",
            "Transport JAG to court hearing",
            "Uncover tainted evidence"
        }));
        singleTypes.Add(new MissionType("Tactical", new List<string> {
            "Admiral on board issues questionable order",
            "Analyze enemy ship construction rates",
            "Analyze weapon signatures",
            "Appointed fleet commander",
            "Blow something up",
            "Borg incursion",
            "Buy evacuees time to escape",
            "Employ damage control teams",
            "Engineer tactical advantage",
            "Ground combat required",
            "Negotiate cease-fire",
            "Orbital bombardment",
            "Plan battle",
            "Provide ally tactical advice",
            "Provide Starfleet ship deployment strategy",
            "Put in place medical and rescue personnel",
            "Reverse-engineer weapons technology",
            "Run tactical drills",
            "Swap executive officers",
            "Use knowledge of local space to gain advantage"
        }));
        missionTypes.AddRange(singleTypes);
        missionTypes.Add(new MultiMissionType(singleTypes));
        #endregion

        #region Incident
        var standaloneIncidents = new List<RandomPick<string>> {
            new RandomPick<string>(new List<string>{"Arrest"}),
            new RandomPick<string>(new List<string>{"Build"}),
            new RandomPick<string>(new List<string>{"Command"}),
            new RandomPick<string>(new List<string>{"Control"}),
            new RandomPick<string>(new List<string>{"Debate"}),
            new RandomPick<string>(new List<string>{"Destroy"}),
            new RandomPick<string>(new List<string>{"Explore"}),
            new RandomPick<string>(new List<string>{"Investigate"}),
            new RandomPick<string>(new List<string>{"Medicate"}),
            new RandomPick<string>(new List<string>{"Navigate"}),
            new RandomPick<string>(new List<string>{"Rescue"}),
            new RandomPick<string>(new List<string>{"Research"}),
            new RandomPick<string>(new List<string>{"Retrieve"}),
            new RandomPick<string>(new List<string>{"Arrest"}),
            new RandomPick<string>(new List<string>{"Save"}),
            new RandomPick<string>(new List<string>{"Survey"}),
            new RandomPick<string>(new List<string>{"Terraform"}),
            new RandomPick<string>(new List<string>{"Unite"}),
        };
        var combineIncidents = new RandomPick2String(standaloneIncidents);
        incidents.AddRange(standaloneIncidents);
        incidents.Add(combineIncidents);
        #endregion

        #region Theme
        var standaloneThemes = new List<RandomPick<string>> {
            new RandomPick<string>(new List<string>{"Ancient ruin"}),
            new RandomPick<string>(new List<string>{"Colony"}),
            new RandomPick<string>(new List<string>{"Crater"}),
            new RandomPick<string>(new List<string>{"Energy being"}),
            new RandomPick<string>(new List<string>{"Exploding planet"}),
            new RandomPick<string>(new List<string>{"Gravimetric distortion"}),
            new RandomPick<string>(new List<string>{"Living ship"}),
            new RandomPick<string>(new List<string>{"Marooned shuttlecraft"}),
            new RandomPick<string>(new List<string>{"Nest"}),
            new RandomPick<string>(new List<string>{"Quarantine zone"}),
            new RandomPick<string>(new List<string>{"Relic"}),
            new RandomPick<string>(new List<string>{"Rogue comet"}),
            new RandomPick<string>(new List<string>{"Rogue superweapon"}),
            new RandomPick<string>(new List<string>{"Salvageable wreck"}),
            new RandomPick<string>(new List<string>{"Sentient machine"}),
            new RandomPick<string>(new List<string>{"Spatial rift"}),
            new RandomPick<string>(new List<string>{"Temporal anomaly"}),
            new RandomPick<string>(new List<string>{"Unknown life-form"}),
        };
        var combineThemes = new RandomPick2String(standaloneThemes);
        themes.AddRange(standaloneThemes);
        themes.Add(combineThemes);
        #endregion

        #region Advantages & Complications
        // Page 258 (PDF 274)
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("A Chance Encounter","An old ally appears in your hour of need or a specialist is delivered by shuttlecraft whose expertise is needed for the mission.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Surplus Components","You find an extra piece of equipment or more supplies that assist you in one of the tasks you encounter.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Surprise Discovery","The situation turns out to not be as grave as initially indicated or an aspect of it can be completed faster than expected.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Remember Your Training","An aspect of Starfleet training pops up in your mind that inspires you to work harder, faster, or more efficiently in the current situation.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Second Wind","A boost of energy helps with dealing with a tense situation and allows you to regain some of the control needed to accomplish your goals.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Helpful Species - Primitive",": Another species comes to your assistance despite not possessing much technical acumen.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Helpful Species - Contemporaries","A species arrives with capabilities similar to your own and offers insight or technical assistance into the situation.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Helpful Species - Advanced"," Possessing sophisticated technology or energy manipulating abilities, this species is able to help the crew in ways they are unable to fathom.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("An Ancient Discovery","Investigating the planet or phenomenon reveals an ancient relic or piece of technology that can help the crew with their mission.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Federation Assistance","Another starship is in range to render assistance with the task at hand.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Change of Heart","A nemesis or superior officer decides to assist the crew, whether for their own purposes or because they have been charmed by the crew.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Redundant Systems","A little-used auxiliary subsystem or spare piece of internal biology helps ease the situation.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Lucky Circumstances","An earlier failure has unintended positive side effects.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Beneficial Element","Something inherent to the environment, such as a rare element or a sympathetic political group, comes to the crew's assistance.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Inspiring Vista","The environment in which the crew operates is picturesque, historic, or provokes strong emotional responses in the crew that motivate them to work harder to save it.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Historic Precedent","Drawing upon past mission logs helps inform the crew as to how to best handle the situation.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Advanced Prototype",": The crew possesses an advanced piece of equipment that is not readily available on other ships but may be of use.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Critical Evidence","Either by discovering information about a mystery on a recovered data drive or fossilized remains giving a clue about an impending environmental disaster, the crew discovers information critical to the success of the mission.")}));
        this.advantages.Add(new RandomPick<Advantage>(new List<Advantage>{ new Advantage("Power Surge","An unexpected surge in energy boosts systems to operate above their threshold or clears up an environmental anomaly.")}));
        this.advantages.Add(new RandomPick2<Advantage>(this.advantages));

        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Total Catastrophe","The situation escalates dramatically, causing the crew to have to work harder and faster than expected.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Inferior Equipment","Complications arise due to poorly maintained gear or not having the right equipment readily available.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Random Failure","A piece of stable equipment suddenly shorts out or a crewmember fails their task in an unexpected way.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Horrific Discovery","A disturbing revelation shakes the crew, forcing them to reevaluate circumstances.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Crisis of Conscience","A crew member suddenly acts against the mission, causing the circumstances to change suddenly.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Unexpected Attack","As the crew works to resolve the crisis at hand, an outside force attacks.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Disease Outbreak","The outbreak of a plague or contagion gravely affects the environment, forcing sanitation to be of even greater concern and more precautions taken to prevent infection.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Environmental Danger","During the mission, something occurs that changes the area around the crew to the point that their lives are in danger.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Sudden Reversal","Despite your earlier success, a change of circumstance means a previous successful test becomes a failure, reversing its affect immediately.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Absolute Bedlam","Where once order reigned, the situation breaks down into chaos as too many factors start to work against the situation.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Conflicting Orders","The situation is made more tense when orders are received that make the crew have to decide between pursuing their stated mission goals or obeying their conscience.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Shattered Chain of Command",": Whether by outside interference or the senior commander being removed from the situation, there is no longer a clear operational command structure for the mission.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Infiltrator","One of the members of the crew has been replaced by an outside agent working against your goal, although their identity has not been revealed yet.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Mesmerized Crew","A strange phenomenon or alien creature has entranced the crew and at least distracts them from their duties or at most renders them completely fascinated and unable to function.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Overworked Crew","The crew is too busy working on a different crisis on the ship and resources are suddenly stretched thin.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Diplomatic Incident","Due to the technicalities of galactic law or a tense political situation, the crew must proceed carefully, or the situation will get worse.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Inept Crewmember","The only specialist the crew has for this situation is secretly unable to proceed with the mission, either due to lack of skill or being too terrified to continue.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("Massive Casualties","Because of an event, many of the crew have been rendered unable to continue to serve the mission and can no longer be counted on.")}));
        this.complications.Add(new RandomPick<Complication>(new List<Complication>{ new Complication("More Valuable Than Useful","An item is uncovered during the mission that is so valuable that multiple governments and private agencies send their agents to retrieve it, despite it not having any immediate usefulness.")}));
        this.complications.Add(new RandomPick2<Complication>(this.complications));
        #endregion
    }

    public MissionBriefing Generate() {
        // Pick random type
        var type = this.missionTypes.Random();
        // Get type & categories
        var bf = type.CreateBriefing();
        // Roll an incident (267) & theme (267) and combine them
        bf.IncitingIncident = string.Join(", ", this.themes.Random().GetRandom()) + "/" + string.Join(", ", this.incidents.Random().GetRandom());
        // Roll starting advantage or complication
        if ((new D20().NumericValue) % 2 == 0) {
            // Even roll advantage
            bf.Advantages = this.advantages.Random().GetRandom().ToArray();
        } else {
            // Odd roll complication
            bf.Complications = this.complications.Random().GetRandom().ToArray();
        }
        // Roll on encounters table to finish inciting incident 

        return bf;
    }
}

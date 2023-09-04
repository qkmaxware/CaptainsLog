namespace CaptainsLog;

public class LogEntryContents {
    public string? WrittenEntry;
}

public class LogEntry {
    public DateTime CreatedOn;
    public DateTime Stardate;
    public string? Title;
    public LogEntryContents? Primary;
    public List<LogEntryContents>? Supplemental;
}

public class PersonalLog {
    public List<LogEntry>? Items;
    public int EntryCount => Items?.Count ?? 0;
}
namespace S7.CustomEvents;
public  struct ReadModel(string dbName, string dbValue)
{
    public string DBName { get; set; } = dbName;

    public string DBValue { get; set; } = dbValue;
}

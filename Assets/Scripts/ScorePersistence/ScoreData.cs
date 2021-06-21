[System.Serializable]
public class ScoreData
{
    public ScoreEntry TopResult;
    public ScoreEntry Result;
    public ScoreData()
    {
        TopResult = new ScoreEntry();
        Result = new ScoreEntry();
    }

    public override string ToString()
    {
        return $"Top Result: [{TopResult}]  |  Result: [{Result}]";
    }
}

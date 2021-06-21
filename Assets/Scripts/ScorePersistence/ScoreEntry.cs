[System.Serializable]
public class ScoreEntry
{
    public string Name;
    public int Score;
    public override string ToString()
    {
        return $"Name: {Name} - Score: {Score}";
    }
}

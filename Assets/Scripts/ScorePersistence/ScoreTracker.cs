using UnityEngine;
using System.IO;

public class ScoreTracker : MonoBehaviour
{
    private ScoreData _scoreData;

    public static ScoreTracker Instance = null;

    private string _path = "/savefile.json";

    void Awake()
    {
        _path = Application.persistentDataPath + _path;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
            Destroy(gameObject);
    }

    public string LastPlayerName =>_scoreData.Result.Name;

    public ScoreEntry TopResult => _scoreData.TopResult;

    public string NewPlayerName
    {
        set
        {
            _scoreData.Result.Name = value;
            _scoreData.Result.Score = 0;
        }
    }

    private void LoadData()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            _scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
        else
            _scoreData = new ScoreData();
    }
    public void SaveData()
    {
        if (_scoreData.TopResult.Score < _scoreData.Result.Score)
        {
            _scoreData.TopResult.Name = _scoreData.Result.Name;
            _scoreData.TopResult.Score = _scoreData.Result.Score;
        }

        string json = JsonUtility.ToJson(_scoreData);

        File.WriteAllText(_path, json);
    }

    public void UpdateCurrentScore(int score)
    {
        _scoreData.Result.Score = score;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Brick _brickPrefab;
    [SerializeField] int _lineCount = 6;
    [SerializeField] Rigidbody _ball;

    [SerializeField] Text _scoreText;
    [SerializeField] Text _bestScoreText;
    [SerializeField] GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < _lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (ScoreTracker.Instance != null)
            _bestScoreText.text = $"Best score: {ScoreTracker.Instance.TopResult.Name} - {ScoreTracker.Instance.TopResult.Score}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        _scoreText.text = $"Score : {m_Points}";
        if (ScoreTracker.Instance != null)
            ScoreTracker.Instance.UpdateCurrentScore(m_Points);
    }

    public void GameOver()
    {
        if (ScoreTracker.Instance != null)
        {
            ScoreTracker.Instance.SaveData();
            UpdateBestScore();
        }
         m_GameOver = true;
        GameOverText.SetActive(true);
    }
}

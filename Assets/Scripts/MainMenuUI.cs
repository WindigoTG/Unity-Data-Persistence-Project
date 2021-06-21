
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _bestScoreText;
    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

    // Start is called before the first frame update
    void Start()
    {
        new GameObject("ScoreTracker").AddComponent<ScoreTracker>();
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);

        SetUpUI();
    }

    private void SetUpUI()
    {
        string name = ScoreTracker.Instance.LastPlayerName;
        if (name != "")
            _nameInput.text = name;

        ScoreEntry topScore = ScoreTracker.Instance.TopResult;
        if (topScore.Name != "" && topScore.Score != 0)
            _bestScoreText.text = $"Best Score: {topScore.Name} - {topScore.Score}";
    }

    private void StartGame()
    {
        if (_nameInput.text != "")
        {
            ScoreTracker.Instance.NewPlayerName = _nameInput.text;
            SceneManager.LoadScene(1);
        }
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
}

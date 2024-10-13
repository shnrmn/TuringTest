using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;
    [SerializeField] private GameObject gameOverScene;

    private GameState _currentState;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;
    private bool _gameOver = false;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        _gameOver = false;
        if (levels.Length > 0)
            ChangeState(GameState.Briefing, levels[_currentLevelIndex]);
    }

    void Update()
    {
        if ( _gameOver && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            // Restart the game
            _gameOver = false;
            _currentLevelIndex = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeState(GameState newState, LevelManager level)
    {
        _currentState = newState;
        _currentLevel = level;

        switch (_currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitiateLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
        }
    }

    public bool IsInputActive()
    {
        return _isInputActive;
    }

    private void StartBriefing()
    {
        Debug.Log("Briefing started");

        // Disable player input
        _isInputActive = false;

        // Start the level
        ChangeState(GameState.LevelStart, _currentLevel);
    }

    private void InitiateLevel()
    {
        Debug.Log("Level starting");

        _isInputActive = true;
        _currentLevel.StartLevel();

        ChangeState(GameState.LevelIn, _currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("Level in: " + _currentLevel.gameObject.name);
    }

    private void CompleteLevel()
    {
        Debug.Log("Level end");
        if (_currentLevelIndex == levels.Length - 1)
        {
            ChangeState(GameState.GameEnd, null);
            return;
        }

        ChangeState(GameState.LevelStart, levels[++_currentLevelIndex]);
    }

    private void GameOver()
    {
        Debug.Log("Game over");
        _gameOver = true;
        gameOverScene.SetActive(true);
    }

    private void GameEnd()
    {
        Debug.Log("Game end");
        _gameOver = true;
    }
}

public enum GameState
{
    Briefing,
    LevelStart,
    LevelIn,
    LevelEnd,
    GameOver,
    GameEnd
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public delegate void ComboEvent(int comboConteur);

    public static GameManager Instance { get; private set; }

    public event GameEvent OnMenu;
    public event GameEvent OnPlay;
    public event GameEvent OnPause;
    public event GameEvent OnUnpause;
    public event GameEvent OnPlayerPlayed;
    public event GameEvent OnGameOver;
    public event GameEvent OnWin;

    public event ComboEvent OnComboIncreased;
    public event GameEvent OnCombotReseted;
    private int ComboLevel;

    [SerializeField]
    private bool startOnPlay;

    public bool OnGame { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(!startOnPlay)
            OnMenu?.Invoke();
    }

    private void Update()
    {
        if(startOnPlay)
        {
            OnPlay?.Invoke();
            startOnPlay = false;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        OnPlay?.Invoke();
        OnGame = true;
    }

    public void Pause(bool _pause)
    {
        if(_pause)
            OnPause?.Invoke();
        else
            OnUnpause?.Invoke();

        Time.timeScale = _pause ? 0f : 1f;
    }

    public void Menu()
    {
        OnGame = false;
        SceneManager.LoadScene(0);
        OnMenu?.Invoke();
    }

    public void GameOver()
    {
        OnGame = false;
        OnGameOver?.Invoke();
    }

    public void Win()
    {
        OnGame = false;
        OnWin?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayerPlayed()
    {
        OnPlayerPlayed?.Invoke();
    }

    public void ComboIncreased()
    {
        ComboLevel++;
        OnComboIncreased?.Invoke(ComboLevel);
    }

    public void CombotReseted()
    {
        ComboLevel = 0;
        OnCombotReseted?.Invoke();
    }

}

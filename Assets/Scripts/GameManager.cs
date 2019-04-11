using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public delegate void ComboEvent(int comboConteur);

    public static GameManager Instance { get; private set; }

    public event GameEvent OnMenu;
    public event GameEvent OnPlay;
    public event GameEvent OnPlayerPlayed;
    public event GameEvent OnGameOver;

    public event ComboEvent OnComboIncreased;
    public event GameEvent OnCombotReseted;
    private int ComboLevel = 0;

    [SerializeField]
    private bool startOnPlay = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        OnMenu?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public static GameManager Instance { get; private set; }

    public event GameEvent OnMenu;
    public event GameEvent OnPlay;
    public event GameEvent OnGameOver;

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
}

using Audio;
using UnityEngine;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pausePanel;

        [SerializeField]
        private GameObject gameoverPanel;

        [SerializeField]
        private GameObject winPanel;

        private void Start()
        {
            GameManager.Instance.OnPause += OnPause;
            GameManager.Instance.OnUnpause += OnUnpause;
            GameManager.Instance.OnGameOver += OnGameOver;
            GameManager.Instance.OnWin += OnWin;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnPause -= OnPause;
            GameManager.Instance.OnUnpause -= OnUnpause;
            GameManager.Instance.OnGameOver -= OnGameOver;
            GameManager.Instance.OnWin -= OnWin;
        }

        private void OnWin()
        {
            winPanel.SetActive(true);
        }

        private void OnGameOver()
        {
            gameoverPanel.SetActive(true);
        }

        private void OnUnpause()
        {
            pausePanel.SetActive(false);
        }

        private void OnPause()
        {
            pausePanel.SetActive(true);
        }

        public void Pause(bool _pause)
        {
            GameManager.Instance.Pause(_pause);
            AudioManager.Instance.PlayButtonEvent();
        }

        public void Menu()
        {
            GameManager.Instance.Menu();
            AudioManager.Instance.PlayButtonEvent();
            Time.timeScale = 1f;
        }
    }
}

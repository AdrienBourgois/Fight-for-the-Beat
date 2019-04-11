using UnityEngine;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pausePanel;

        private void Start()
        {
            GameManager.Instance.OnPause += () => pausePanel.SetActive(true);
            GameManager.Instance.OnUnpause += () => pausePanel.SetActive(false);
        }

        public void Pause(bool _pause)
        {
            GameManager.Instance.Pause(_pause);
        }

        public void Menu()
        {
            GameManager.Instance.Menu();
        }
    }
}

using Audio;
using UnityEngine;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public void Play() => GameManager.Instance.Play();
        public void Menu() => GameManager.Instance.Menu();

        public void Quit() => GameManager.Instance.Quit();

        public void SetMusicVolume(float _volume) => AudioManager.Instance.SetMusicVolume(_volume);
        public void SetFxVolume(float _volume) => AudioManager.Instance.SetFxVolume(_volume);
    }
}

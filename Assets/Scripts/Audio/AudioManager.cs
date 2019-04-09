using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public float MusicVolume { get; private set; } = 1f;
        public float FxVolume { get; private set; } = 1f;

        [SerializeField]
        [EventRef]
        private string menuEvent = "";

        [SerializeField]
        [EventRef]
        private string musicEvent = "";

        private EventInstance eventInstance;

        private int beat;

        public void SetMusicVolume(float _volume)
        {
            MusicVolume = _volume;
            if (eventInstance.isValid())
                eventInstance.setVolume(_volume);
        }

        public void SetFxVolume(float _volume) => FxVolume = _volume;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameManager.Instance.OnMenu += OnMenu;
            GameManager.Instance.OnPlay += OnPlay;
        }

        private void OnPlay()
        {
            SetInstance(musicEvent);
        }

        private void OnMenu()
        {
            SetInstance(menuEvent);
        }

        private void SetInstance(string _event)
        {
            if(eventInstance.isValid())
                eventInstance.release();

            eventInstance = RuntimeManager.CreateInstance(_event);
            eventInstance.start();
        }

        private void Update()
        {
            eventInstance.getTimelinePosition(out int position);
            float time = position / 1000f;
            int total_beat = (int)(time / (60f / 120f));
            beat = total_beat % 4 + 1;
        }
    }
}

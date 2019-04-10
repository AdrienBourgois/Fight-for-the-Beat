using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public const int Bpm = 120;
        public const int BeatCount = 4;

        public delegate void BeatEvent(int _beat);

        public event BeatEvent OnBeat;
        public event BeatEvent OnOffbeat;

        public float MusicVolume { get; private set; } = 1f;
        public float FxVolume { get; private set; } = 1f;

        public int Beat
        {
            get { return beat; }
            set
            {
                if (beat != value)
                    OnBeat?.Invoke(value);
                beat = value;
            }
        }

        public int Offbeat
        {
            get { return offbeat; }
            set
            {
                if (offbeat != value)
                    OnOffbeat?.Invoke(value);
                offbeat = value;
            }
        }

        [SerializeField]
        [EventRef]
        private string menuEvent = "";

        [SerializeField]
        [EventRef]
        private string musicEvent = "";

        private EventInstance eventInstance;

        private int beat;
        private int offbeat;
        private bool updateBeat = false;

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
            GameManager.Instance.OnGameOver += () => updateBeat = false;
        }

        private void OnPlay()
        {
            SetInstance(musicEvent);
            updateBeat = true;
        }

        private void OnMenu()
        {
            SetInstance(menuEvent);
            updateBeat = false;
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
            if (!updateBeat)
                return;

            eventInstance.getTimelinePosition(out int position);
            float time = position / 1000f;
            float total_beat = time / (60f / Bpm);
            Beat = (int)total_beat % BeatCount + 1;
            Offbeat = (int)(total_beat + 60f / Bpm) % BeatCount + 1;
        }
    }
}

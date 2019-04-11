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

        [Header("Musics")]
        [SerializeField]
        [EventRef]
        private string menuEvent = "";

        [SerializeField]
        [EventRef]
        private string musicEvent = "";

        [SerializeField]
        [EventRef]
        private string ambianceEvent = "";

        [Header("UI Sounds")]
        [SerializeField]
        [EventRef]
        private string buttonSelectionEvent = "";

        [SerializeField]
        [EventRef]
        private string pauseEvent = "";

        [SerializeField]
        [EventRef]
        private string unpauseEvent = "";

        [SerializeField]
        [EventRef]
        private string gameoverEvent;

        [Header("Player Sounds")]
        [SerializeField]
        [EventRef]
        private string hitEvent = "";

        [SerializeField]
        [EventRef]
        private string jumpEvent = "";

        [SerializeField]
        [EventRef]
        private string dieEvent = "";

        [SerializeField]
        [EventRef]
        private string walkEvent = "";

        [SerializeField]
        [EventRef]
        private string attackEvent = "";

        [SerializeField]
        [EventRef]
        private string comboResetEvent = "";

        private EventInstance menuInstance;
        private EventInstance musicInstance;
        private EventInstance ambianceInstance;

        private int beat;
        private int offbeat;
        private bool updateBeat;

        public void SetMusicVolume(float _volume)
        {
            MusicVolume = _volume;
            if (menuInstance.isValid())
                menuInstance.setVolume(_volume);
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
            GameManager.Instance.OnPause += OnPause;
            GameManager.Instance.OnUnpause += OnUnpause;
            GameManager.Instance.OnComboIncreased += OnComboChange;

            GameManager.Instance.OnCombotReseted += () =>
            {
                OnComboChange(0);
                PlayOneShot(comboResetEvent);
            };

            GameManager.Instance.OnGameOver += () =>
            {
                updateBeat = false;
                PlayOneShot(gameoverEvent);
                PlayOneShot(dieEvent);
            };
        }

        private void OnComboChange(int _combo)
        {
            musicInstance.setParameterValue("Main", _combo > 3 ? 3f : _combo + 1);
        }

        private void OnPause()
        {
            musicInstance.setPaused(true);
            PlayOneShot(pauseEvent);
            updateBeat = false;
        }

        private void OnUnpause()
        {
            musicInstance.setPaused(false);
            PlayOneShot(unpauseEvent);
            updateBeat = true;
        }

        private void OnPlay()
        {
            menuInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            if (!musicInstance.isValid())
                musicInstance = RuntimeManager.CreateInstance(musicEvent);

            if (!ambianceInstance.isValid())
                ambianceInstance = RuntimeManager.CreateInstance(ambianceEvent);

            musicInstance.setParameterValue("Main", 1f);
            ambianceInstance.setParameterValue("Main", 1f);

            musicInstance.start();
            ambianceInstance.start();

            updateBeat = true;
        }

        private void OnMenu()
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambianceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            if (!menuInstance.isValid())
                menuInstance = RuntimeManager.CreateInstance(menuEvent);

            menuInstance.start();

            updateBeat = false;
        }

        private void Update()
        {
            if (updateBeat)
                UpdateBeat();
        }

        private void UpdateBeat()
        {
            musicInstance.getTimelinePosition(out int position);
            float time = position / 1000f;
            float total_beat = time / (60f / Bpm);
            Beat = (int)total_beat % BeatCount + 1;
            Offbeat = (int)(total_beat + 60f / Bpm) % BeatCount + 1;
        }

        public static void PlayOneShot(string _event) => RuntimeManager.PlayOneShot(_event);

        public void PlayButtonEvent() => PlayOneShot(buttonSelectionEvent);
        public void PlayHitEvent() => PlayOneShot(hitEvent);
        public void PlayJumpEvent() => PlayOneShot(jumpEvent);
        public void PlayAttackEvent() => PlayOneShot(attackEvent);
        public void PlayWalkEvent() => PlayOneShot(walkEvent);
    }
}

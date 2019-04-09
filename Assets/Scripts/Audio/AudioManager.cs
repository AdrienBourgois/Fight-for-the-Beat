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
        private EventInstance menuEventInstance;

        private int beat;

        public void SetMusicVolume(float _volume)
        {
            MusicVolume = _volume;
            if (menuEventInstance.isValid())
                menuEventInstance.setVolume(_volume);
        }

        public void SetFxVolume(float _volume) => FxVolume = _volume;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            menuEventInstance = RuntimeManager.CreateInstance(menuEvent);
            menuEventInstance.start();
        }

        private void Update()
        {
            menuEventInstance.getTimelinePosition(out int position);
            float time = position / 1000f;
            int total_beat = (int)(time / (60f / 120f));
            beat = total_beat % 4 + 1;
        }
    }
}

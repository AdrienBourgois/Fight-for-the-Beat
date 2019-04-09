using System;
using AOT;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class SoundOneShot : MonoBehaviour
    {
        private EVENT_CALLBACK endCallback;
        private EventInstance soundInstance;
        private bool mustBeDestroyed;

        public static void PlayOneShot(string _event, float _volume = 1f)
        {
            GameObject go = new GameObject("Sound " + _event);
            SoundOneShot sound = go.AddComponent<SoundOneShot>();
            sound.SetParametersAndPlay(_event, _volume * AudioManager.Instance.FxVolume);
        }

        private void SetParametersAndPlay(string _event, float _volume)
        {
            endCallback = OnStopped;

            soundInstance = RuntimeManager.CreateInstance(_event);

            transform.position = Vector3.zero;

            soundInstance.setVolume(_volume);
            soundInstance.setCallback(endCallback, EVENT_CALLBACK_TYPE.STOPPED);
            soundInstance.start();
        }

        private void Update()
        {
            if(mustBeDestroyed)
                Destroy(gameObject);
        }

        [MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
        private RESULT OnStopped(EVENT_CALLBACK_TYPE _type, EventInstance _eventInstance, IntPtr _parameters)
        {
            soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            soundInstance.release();
            mustBeDestroyed = true;
            return RESULT.OK;
        }
    }
}

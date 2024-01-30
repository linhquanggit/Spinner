using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spinner
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<AudioManager>();
                return instance;
            }
        }

        [SerializeField] private AudioSource src;
        [SerializeField] private AudioClip spinClip;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
        public void PlaySpinClip(bool play, float pitchValue)
        {
            if (!play)
                src.Stop();
            else
            {
                src.clip = spinClip;
                float normalizedPitch = Mathf.Lerp(0f, 2f, Mathf.Abs(pitchValue) / 20f);
                src.pitch = normalizedPitch;
                if (!src.isPlaying)
                src.Play();
            }    
        }

    }
}
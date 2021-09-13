using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public class SoundPlayer : MonoBehaviour
    {
        public float minPitch = .8f;
        public float maxPitch = 1.2f;
        
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private float GetPitch()
        {
            return Random.Range(minPitch, maxPitch);
        }

        public void Play(AudioClip clip)
        {
            _audioSource.pitch = GetPitch();
            _audioSource.PlayOneShot(clip);
        }
        
    }
}

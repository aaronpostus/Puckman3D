using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace OttiPostLewis.Lab6
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip pacmanEating;
        private AudioSource audioSource;
        private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            sounds.Add("Eat", pacmanEating);
        }

        public void PlaySound(string soundID)
        {
            AudioClip currentSound;
            if (sounds.TryGetValue(soundID, out currentSound))
            {
                audioSource.clip = currentSound;
                audioSource.Play();
            }
        }

    }
}
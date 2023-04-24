using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace OttiPostLewis.Lab6
{
    public class SoundManager : MonoBehaviour
    {
        private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
        private AudioSource bgMusicSource, fxSource;
        [SerializeField] private AudioClip pacmanEating, backGroundMusic, pacmanDeath, pacmanEatGhost;
        private bool bgMusicIsPlaying = false;

        void Start()
        {
            bgMusicSource = gameObject.AddComponent<AudioSource>();
            fxSource = gameObject.AddComponent<AudioSource>();

            bgMusicSource.volume = 0.3f;
            bgMusicSource.loop = true;
            bgMusicSource.clip = backGroundMusic;
            

            sounds.Add("Eat", pacmanEating);
            sounds.Add("Death", pacmanDeath);
            sounds.Add("EatGhost", pacmanEatGhost);
            BackgroundMusic();
        }

        public void PlaySound(string soundID)
        {
            AudioClip currentSound;
            if (sounds.TryGetValue(soundID, out currentSound))
            {
                fxSource.clip = currentSound;
                fxSource.Play();
            }
        }



        public void BackgroundMusic()
        {
            
            if (bgMusicIsPlaying)
            {
                bgMusicSource.Stop();
            }
            else
            {
                bgMusicSource.Play();
            }

            bgMusicIsPlaying = !bgMusicIsPlaying;


        }

    }
}
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
        [SerializeField] private AudioClip pacmanEatPellet, backGroundMusic, pacmanDeath, pacmanEatSuperPellet;
        private bool bgMusicIsPlaying = false;

        void Start()
        {
            bgMusicSource = gameObject.AddComponent<AudioSource>();
            fxSource = gameObject.AddComponent<AudioSource>();

            bgMusicSource.volume = 0.2f;
            bgMusicSource.loop = true;
            bgMusicSource.clip = backGroundMusic;
            

            sounds.Add("Eat", pacmanEatPellet);
            sounds.Add("Death", pacmanDeath);
            sounds.Add("EatSuperPellet", pacmanEatSuperPellet);
            BackgroundMusic();
        }
        //This method plays a sound effect based on the sound ID
        public void PlaySound(string soundID)
        {
            AudioClip currentSound;
            //Checl of spimd exists in dictionary
            if (sounds.TryGetValue(soundID, out currentSound))
            {
                fxSource.clip = currentSound;
                fxSource.Play();
            }
        }


        //This method toggles the background music on and off
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
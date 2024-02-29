using System;
using System.Collections;
using UnityEngine;

namespace SFX
{
    public class PlayerAudioManager : MonoBehaviour
    {
        public static PlayerAudioManager instance;

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);

            if (s == null)
            {
                Debug.LogError("Music not found");
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);

            if (s == null)
            {
                Debug.LogError("Sound not found");
            }
            else
            {
                sfxSource.PlayOneShot(s.clip);
                StartCoroutine(ResetSFXPlayingStatus(s.clip.length));
            }
        }

        private IEnumerator ResetSFXPlayingStatus(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static float musicVolume
    {
        get { return _musicVolume; }
        set {            
            _musicVolume = value;
            UpdateMusicVolume();
        }
    }
    public static float masterVolume
    {
        get { return _masterVolume; }
        set
        {
            _masterVolume = value;
            UpdateMusicVolume();
        }
    }

    private static float _musicVolume;
    private static float _masterVolume;
    public static float sfxVolume;

    public SoundAudioClip[] soundAudioClips;
    static SoundAudioClip[] _soundAudioClips;

    public float mainMusicVolume = 1;
    static float _mainMusicVolume;
    static GameObject mainMusicGameObject;

    public enum Sound { 
        DefaultShoot,
        RifleShoot,
        RPGShoot,
        Damage,
        BarrelExplosion,
        PlaneExplosion,
        BalloonPickup,
        GameOver,
        PlaneSuspension
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public SoundInfo soundInfo;
    }
    [System.Serializable]
    public class SoundInfo {
        public AudioClip audioClip;
        [Range(0,1)]
        public float volume;
    }



    private void Awake()
    {
        _soundAudioClips = soundAudioClips;
        mainMusicGameObject = gameObject;
        _mainMusicVolume = mainMusicVolume;

        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
    }

    public static void PlaySound(Sound sound)
    {
        PlaySound(GetSoundInfo(sound));
    }
    public static void PlaySound(Sound sound, Vector3 position)
    {
        PlaySound(GetSoundInfo(sound), position);
    }

    public static void PlayMusic(AudioClip audioClip, float volume)
    {
        GameObject soundGameObject = new GameObject("Music");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume * musicVolume * masterVolume;
        audioSource.PlayOneShot(audioClip);
    }

    public static void PlaySound(SoundInfo soundInfo)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = sfxVolume * soundInfo.volume * masterVolume;
        audioSource.PlayOneShot(soundInfo.audioClip);
    }
    public static void PlaySound(SoundInfo soundInfo, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = soundInfo.audioClip;
        audioSource.volume = sfxVolume * soundInfo.volume * masterVolume;
        audioSource.Play();
    }
    static SoundInfo GetSoundInfo(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in _soundAudioClips){
            if (soundAudioClip.sound == sound) return soundAudioClip.soundInfo;
        }
        Debug.LogError("Nie znaleziono dzwiêku!");
        return null;
    }
    static void UpdateMusicVolume()
    {
        mainMusicGameObject.GetComponent<AudioSource>().volume = musicVolume * _mainMusicVolume * masterVolume;
    }
}

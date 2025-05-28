using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFx;
    [SerializeField] private AudioSource music;
    [SerializeField] private Sound[] sounds;
    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void PlaySound(SoundType soundType)
    {
        Sound sound = Array.Find(sounds, item => item.soundType == soundType);
        if (soundType == SoundType.BGMusic || soundType == SoundType.GameOver || soundType == SoundType.FinishLevel || soundType == SoundType.MenuMusic)
        {
            music.clip = sound.audioClip;
            music.Play();
        }
        else
        {
            soundFx.PlayOneShot(sound.audioClip);
        }
    }
    public float GetSfxVolume()
    {
        return soundFx.volume;
    }
    public void SetSfxVolume(float _volume)
    {
        soundFx.volume = _volume;
    }
    public float GetMusicVolume()
    {
        return music.volume;
    }
    public void SetMusicVolume(float _volume)
    {
        music.volume = _volume;
    }
    public void PauseMusic()
    {
        Debug.Log("Pause music called!");
        music.Pause();
    }
    public void ResumeMusic()
    {
        Debug.Log("Resume music called!");
        music.Play();
    }
}
[Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip audioClip;
}
public enum SoundType
{
    ButtonClick, BackButton, BGMusic, GameOver, FinishLevel, Jump, Coin, MenuMusic, Machine, PlayerSwitch
}


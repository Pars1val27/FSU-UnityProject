using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{

    static public Audio audioInstance;
    [Header("----Audios----")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] clickSounds;
    [SerializeField] AudioClip[] Background;
    [SerializeField] AudioClip[] Boss;
    [SerializeField] AudioClip[] sliderSounds;
    [SerializeField] AudioClip lowTime;

    [Header("----Sliders----")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


    float masterVol;
    float musicVol;
    float sfxVol;
    // Start is called before the first frame update
    void Start()
    {
        audioInstance = this;
        if (PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVol();
            SetMusicVol();
            SetPlayerVol();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVol()
    {
        masterVol = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(masterVol)*20);
        PlayerPrefs.SetFloat("MasterVolume", masterVol);
    }

    public void SetMusicVol()
    {
        musicVol = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVol)*20);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
    }

    public void SetPlayerVol()
    {
        sfxVol = sfxSlider.value;
        audioMixer.SetFloat("Player", Mathf.Log10(sfxVol)*20);
        PlayerPrefs.SetFloat("SFXVolume", sfxVol);
    }

    void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMasterVol();
        SetMusicVol();
        SetPlayerVol();

    }

    public void PlayClicks()
    {
        sfxSource.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Length)], sfxVol);
        Debug.Log("Click Sound Played");
    }

    public void PlayBackground(int index)
    {
        musicSource.clip = Background[index];
        musicSource.Play();
    }

    public void PlayBoss(int index)
    { 
        musicSource.clip = Boss[index];
        musicSource.Play();
    }

    public void PlaySlider()
    {
        sfxSource.clip = sliderSounds[Random.Range(0, sliderSounds.Length)];
        sfxSource.Play();
    }

    public void PlayLowTime()
    {
        musicSource.clip = lowTime;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void PlayMusic()
    {
        musicSource.UnPause();
    }
}

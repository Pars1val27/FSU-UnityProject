using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [Header("----Audios----")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] clickSounds;
    [SerializeField] Audio[] Background;
    [SerializeField] Audio[] Boss;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVol()
    {
        masterVol = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(masterVol)*20);
    }

    public void SetMusicVol()
    {
        musicVol = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVol)*20);
    }

    public void SetPlayerVol()
    {
        sfxVol = sfxSlider.value;
        audioMixer.SetFloat("Player", Mathf.Log10(sfxVol)*20);
    }

    public void PlayClicks()
    {
        aud.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Length)], masterVol);
    }

    public void PlayBackground()
    {
        
    }

    public void PlayBoss()
    {

    }
}

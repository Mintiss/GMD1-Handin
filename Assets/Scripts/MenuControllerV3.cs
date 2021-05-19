using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerV3 : MonoBehaviour
{

    public Text volume;
    public AudioSource audioSource;
    public AudioClip mainMenu;
    private float volumeFloat;

    public void Quit()
    {
        Application.Quit();
        print("GameClosed");
    }

    public void Play()
    {
        SceneManager.LoadScene("Intro");
        audioSource.Stop();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume", float.Parse(volume.text));
    }


    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        mainMenu = audioSource.clip;
        volumeFloat = PlayerPrefs.GetFloat("volume");
        audioSource.PlayOneShot(mainMenu, volumeFloat);
    }

    // Update is called once per frame
    void Update()
    {
        VolumeCheck();
    }

    void VolumeCheck()
    {
        if (volumeFloat != PlayerPrefs.GetFloat("volume")) 
        {
            volumeFloat = PlayerPrefs.GetFloat("volume");
            PlayerPrefs.SetFloat("volume", volumeFloat);
            audioSource.volume = volumeFloat;
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip background;
    public float volume;

    void Awake()
    {

        volume = PlayerPrefs.GetFloat("volume");
        audioSource.PlayOneShot(background, volume);
    }
    public void BackToMenu()
    {
        audioSource.Stop();
        SceneManager.LoadScene("MainMenuV3");
    }
}

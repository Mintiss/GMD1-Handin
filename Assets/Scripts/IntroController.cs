using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip intro;
    public float volume;

    void Awake()
    {

        volume = PlayerPrefs.GetFloat("volume");
        audioSource.PlayOneShot(intro, volume);
    }
    public void Continue()
    {
        audioSource.Stop();
        SceneManager.LoadScene("City");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class ObjectiveController : MonoBehaviour
{
    public TextMeshProUGUI killCount, componentCount, currentObjective;
    public int componentsTaken, kills;
    public GameObject bomb, clubDoor;
    public ParticleSystem explosion;

    //Audio
    public AudioSource audioSource;
    public AudioClip background, club, explosionSound;
    public float volume;

    private IEnumerator coroutine;
    private bool nextToClubDoor, bombCrafted, gameInit, bombPlanted, bombExploded, ratDead;

    // Start is called before the first frame update

    void Awake()
    {

        volume = PlayerPrefs.GetFloat("volume");
        audioSource.PlayOneShot(background, volume);
    }

    void Start()
    {
        bombCrafted = false;
        ratDead = false;
        bombPlanted = false;
        componentsTaken = 0;
        kills = 0;
        nextToClubDoor = false;
        bombExploded = false;
        gameInit = false;
        SetComponentCount();
        SetCurrentObjective();
        bomb.SetActive(false);
        explosion.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Component"))
        {
            other.gameObject.SetActive(false);
            componentsTaken++;
            SetComponentCount();
            SetCurrentObjective();
        }
        if (other.gameObject.CompareTag("ClubDoor"))
        {
            nextToClubDoor = true;
            SetCurrentObjective();
        }
        if (other.gameObject.CompareTag("NPC"))
        {
            other.gameObject.tag = "Untagged";
            kills++;
            killCount.text = "KillCount: " + kills;
        }
        if (other.gameObject.CompareTag("Ho"))
        {
            other.gameObject.tag = "Untagged";
            kills++;
            killCount.text = "KillCount: " + kills;
        }
        if (other.gameObject.CompareTag("Rat"))
        {
            ratDead = true;
            other.gameObject.tag = "Untagged";
            kills++;
            killCount.text = "KillCount: " + kills;
            StopCoroutine(coroutine);
            coroutine = Winner(5.0f);
            StartCoroutine(coroutine);

        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("ClubDoor"))
        {
            nextToClubDoor = false;
            if(bombExploded)
            {
                componentCount.text = "";
                bomb.SetActive(false);
            }
            else if (bombPlanted)
            {
                bomb.SetActive(true);
            }
            else if (bombCrafted)
            {
                StopCoroutine(coroutine);
                bomb.SetActive(false);
                componentCount.text = "Bomb Crafted!";
            }
        }
    }



    private void SetComponentCount()
    {
        componentCount.text = "Components " + componentsTaken.ToString() + " / 4";
    }
    
    private void SetCurrentObjective()
    {
        if (!ratDead)
        {
            if (gameInit == false && nextToClubDoor == false)
            {
                currentObjective.text = "Objective: Get into the club to kill the RAT";
                componentCount.text = "";
            }

            else if (componentsTaken <= 3 && nextToClubDoor)
            {
                coroutine = GameStart(3.0f);
                StartCoroutine(coroutine);
            }

            else if (componentsTaken >= 4 && bombCrafted == false)
            {
                coroutine = GotAllComponents(5.0f);
                StartCoroutine(coroutine);
            }

            else if (nextToClubDoor && bombCrafted && bombPlanted == false)
            {
                coroutine = PlantBomb(5.0f);
                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator GotAllComponents(float waitTime)
    {
        bool Active = true;

        while (Active)
        {
            componentCount.text = "Crafting bomb...";
            currentObjective.text = "Objective: Wait for bomb to craft";
            yield return new WaitForSeconds(waitTime);
            componentCount.text = "Bomb Crafted!";
            bombCrafted = true;
            currentObjective.text = "Objective: Blow up the door to the club";
            Active = false;
        }
    }

    private IEnumerator GameStart(float waitTime)
    {
        bool Active = true;

        while (Active)
        {
            currentObjective.text = "Hmmmm, the door is locked";
            yield return new WaitForSeconds(waitTime);
            currentObjective.text = "Maybe I can blow it down by making an improvised bomb?";
            yield return new WaitForSeconds(waitTime);
            currentObjective.text = "Objective: Find bomb materials in the city";

            SetComponentCount();
            gameInit = true;
            Active = false;

        }
    }

    private IEnumerator PlantBomb(float waitTime)
    {
        bool Active = true;

        while (Active)
        {
            componentCount.text = "Planting bomb...";
            currentObjective.text = "Objective: Plant the bomb on the door";
            bomb.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            componentCount.text = "Bomb Planted!";
            bombPlanted = true;
            audioSource.Stop();
            audioSource.PlayOneShot(club, volume);
            for (int i = 0; i < 5; i++) {
                componentCount.text = (i+1) + "...";
                yield return new WaitForSeconds(waitTime / 5);
            }
            audioSource.PlayOneShot(explosionSound, volume);
            explosion.Play();
            bombExploded = true;
            componentCount.text = "";
            clubDoor.SetActive(false);
            bomb.SetActive(false);
            currentObjective.text = "Objective: Kill the RAT";
            Active = false;
        }
    }

    private IEnumerator Winner(float waitTime)
    {
        bool Active = true;

        while (Active)
        {
            componentCount.text = "";
            currentObjective.text = "You have killed the Rat and avenged your father!";
            yield return new WaitForSeconds(waitTime);
            audioSource.Stop();
            SceneManager.LoadScene("Winner");
            Active = false;
        }
    }
}

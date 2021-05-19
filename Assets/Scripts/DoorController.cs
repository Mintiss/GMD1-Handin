using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator candyAnimator, gunAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunDoor"))
        {
            gunAnimator.Play("GunDoorOpen");
        }
        if (other.gameObject.CompareTag("CandyDoor"))
        {
            candyAnimator.Play("CandyDoorOpen");
        }
    }
}

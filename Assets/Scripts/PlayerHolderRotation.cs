using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolderRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation =Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, player.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}

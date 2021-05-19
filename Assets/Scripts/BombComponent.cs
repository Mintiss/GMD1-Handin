using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(gameObject.name.Equals("OilCan4"))
            transform.Rotate(new Vector3(0, 0, 1), Space.Self);
        else
            transform.Rotate(new Vector3(0, 1, 0), Space.Self);


    }

}

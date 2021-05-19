using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    private Rigidbody capsuleRb;
    public float sideRollSpeed = 60f;
    public float forwardRollSpeed = 50f;
    public float twistSpeed = 50f;
    private ForceMode forceMode = ForceMode.Acceleration;

    void Start()
    {
        capsuleRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 lineStart = transform.position;
        Vector3 sideAxis = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
        sideAxis = Vector3.ProjectOnPlane(sideAxis, Vector3.up);
        sideAxis = sideAxis.normalized;
        Debug.DrawLine(lineStart, lineStart + (sideAxis * 2f), Color.blue, Time.deltaTime);


        if (Input.GetKey(KeyCode.D))
        {
            capsuleRb.AddTorque(Vector3.up * forwardRollSpeed, forceMode);//twist
        }
        if (Input.GetKey(KeyCode.A))
        {
            capsuleRb.AddTorque(-Vector3.up * forwardRollSpeed, forceMode);//twist
        }
        if (Input.GetKey(KeyCode.E))
        {
            capsuleRb.AddTorque(-capsuleRb.transform.forward * sideRollSpeed, forceMode);//roll
        }
        if (Input.GetKey(KeyCode.Q))
        {
            capsuleRb.AddTorque(capsuleRb.transform.forward * sideRollSpeed, forceMode);//roll
        }
        if (Input.GetKey(KeyCode.W))
        {
            capsuleRb.AddTorque(-sideAxis * twistSpeed, forceMode);//tumble
        }
        if (Input.GetKey(KeyCode.S))
        {
            capsuleRb.AddTorque(sideAxis * twistSpeed, forceMode);//tumble
        }
    }
}
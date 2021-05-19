using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubNpcController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update // zbound (-1, -147) xbound (77, 6)
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        setColliderState(false);
        setRigidBodyState(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    public void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;

    }

    public void Die()
    {
        animator.enabled = false;
        setColliderState(true);
        setRigidBodyState(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tire"))
            Die();
    }
}

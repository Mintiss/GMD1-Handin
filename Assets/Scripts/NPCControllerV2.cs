using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControllerV2 : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool walkPointSet;
    private Vector3 walkPoint;
    public Animator animator;

    // Start is called before the first frame update // zbound (-1, -147) xbound (77, 6)
    void Start()
    {
        walkPointSet = false;
        setColliderState(false);
        setRigidBodyState(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag.Equals("NPC"))
        {
            WalkAround();
            animator.Play("Walking");
        }       
    }

    public void WalkAround()
    {
        if (!walkPointSet)
        {
            walkPoint = GeneratePoint();
            agent.SetDestination(walkPoint);
            walkPointSet = true;
        }

        

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    public Vector3 GeneratePoint()
    {
        return new Vector3(Random.Range(6, 77), transform.position.y, Random.Range(-1, -147));
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
        agent.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tire"))
            Die();       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TireControllerV5 : MonoBehaviour
{
    public CharacterController cc;
    public Animator animator;
    public float speed = 2, rotationSpeed = 2, gravity = -9.81f, groundDistance = 0.4f, jumpHeight = 3f;

    public Vector3 velocity, moveInput;
    public Transform groundCheck;
    public LayerMask groundMask;

    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        DetectInput();

        Rotate();

        Animate();

        Move();

        Fall();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2;
    }

    private void DetectInput()
    {
        //Moving
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Rotate()
    {
        transform.forward = Quaternion.Euler(0, moveInput.x * rotationSpeed, 0) * transform.forward;
    }

    private void Fall()
    {
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        cc.SimpleMove(moveInput.z * transform.forward * speed);
    }

    void Animate()
    {
        if (moveInput.z > 0 || velocity.y > 0)
            animator.Play("Forward");
        else if (moveInput.z < 0)
            animator.Play("Backward");
        else
        {
            if (moveInput.x != 0)
                animator.Play("Turn");
            else
                animator.Play("Idle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
        }
    }
}

using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource WalkingSF;

    bool isGrounded;

    static public bool dialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!dialogue) { 
            MovePlayer();
        }

    }

    void MovePlayer() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        //Debug.Log(isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Console.WriteLine("jumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && (x != 0 || z != 0))
        {
            if (!WalkingSF.isPlaying)
            {
                WalkingSF.Play();
                Console.WriteLine("footsteps");
            }
        }
        else
        {
            if (WalkingSF.isPlaying)
            {
                WalkingSF.Stop();
            }
        }
    }
}

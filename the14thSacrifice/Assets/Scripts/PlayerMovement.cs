using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintSpeed = 20;
    private float speedtemp;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource WalkingSF;

    bool isGrounded;

    static public bool dialogue = false;

    public static bool isAttacking = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speedtemp = speed;
    }

    void Update()
    {

        if (!dialogue && !isAttacking) // Prevent movement while attacking
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
                animator.SetBool("Running", true);
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = speedtemp;
                animator.SetBool("Running", false);
            }
            MovePlayer();
        }
        else if(!dialogue && isAttacking){
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
        }
    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Debug.Log(x);

        if (x != 0 || z != 0)
        {
            animator.SetBool("Walking", true);//moving
        }
        else {
            animator.SetBool("Walking", false);
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && (x != 0 || z != 0))
        {
            if (!WalkingSF.isPlaying)
            {
                WalkingSF.Play();
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

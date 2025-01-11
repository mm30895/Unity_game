using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isAttacking = Input.GetButtonDown("Fire1");

        // Set Walking and Running states
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Running", isRunning);
        animator.SetBool("Attacking", isAttacking);

        //// Handle Attack animation
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    animator.SetBool("Attacking", true);
        //}
        //else {
        //    animator.SetBool("Attacking", false);
        //}
    }
}

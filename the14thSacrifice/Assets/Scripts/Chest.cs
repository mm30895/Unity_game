using UnityEngine;

public class Chest : MonoBehaviour
{

    public GameObject eCanvas;
    public Animator animator;

    public SphereCollider collider;

    public GameObject boringSword;
    public GameObject coolSword;

    public PlayerAttack Player;

    private bool chestopened = false;

    bool playerDetection = false;

    public AudioSource chestSF;
    public AudioSource swordSF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && chestopened)
        {
            Debug.Log("pickup sword");
            playerDetection = false;
            eCanvas.SetActive(false);
            coolSword.SetActive(true);
            boringSword.SetActive(false);
            Player.setDamage(20);
            Destroy(collider);
            swordSF.Play();
        }
        if (playerDetection && Input.GetKeyDown(KeyCode.E) && !PlayerMovement.dialogue)
        {
            Debug.Log("chest opens");
            animator.SetTrigger("open");
            chestopened = true;
            chestSF.Play();
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "FirstPersonPlayer")
        {
            eCanvas.SetActive(true);
            playerDetection = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerDetection = false;
        eCanvas.SetActive(false);
    }
}

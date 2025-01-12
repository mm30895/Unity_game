using UnityEngine;
using TMPro;

public class NPCSystem : MonoBehaviour
{
    public GameObject eCanvas;
    public GameObject dTemplate;
    public GameObject canvas;
    bool playerDetection = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection && Input.GetKeyDown(KeyCode.E) && !PlayerMovement.dialogue) {
            Debug.Log("talk");

            ClearOldDialogue();

            canvas.SetActive(true);
            PlayerMovement.dialogue = true;
            NewDialogue("Another one... the fourteenth, they say.");
            NewDialogue("You are to be the next sacrifice for the beast, sent into the labyrinth like all the others.");
            NewDialogue("The Minotaur awaits, and its hunger is endless. No one has ever returned.");
            NewDialogue("But... there is a rumor. A weapon hidden within the maze. A sword, forged to slay even that monster.");
            NewDialogue("It is no mere weapon—it's said to glow with power, a light that cuts through the darkness.");
            NewDialogue("But many have sought it, and none have claimed it. They fell to the minions, or the beast itself.");
            NewDialogue("You must choose: face your fate with that poor sword of yours, or gamble your life searching for this sword.");
            NewDialogue("If you succeed, you could end this nightmare for all of us. But fail...");
            NewDialogue("...and you will join the countless souls that haunt the labyrinth's halls.");
            NewDialogue("The labyrinth is cruel. Trust nothing. And remember, the Minotaur can sense your fear.");
            NewDialogue("Good luck... not that it’s ever been enough.");

            canvas.transform.GetChild(1).gameObject.SetActive(true);

        }
        
    }
    //create new dialogue
    void NewDialogue(string text) { 
        GameObject templateClone = Instantiate(dTemplate, dTemplate.transform);
        templateClone.transform.parent = canvas.transform;
        templateClone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        templateClone.SetActive(false);
    }

    void ClearOldDialogue()
    {
        // Destroy all child objects except the template
        for (int i = canvas.transform.childCount - 1; i >= 2; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "FirstPersonPlayer") { 
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

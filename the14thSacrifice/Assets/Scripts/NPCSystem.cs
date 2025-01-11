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
            NewDialogue("Hello");
            NewDialogue("this is a test");
            NewDialogue("hola muchcho");

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

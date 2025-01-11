using UnityEngine;

public class CombatZoneTrigger : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource CombatMusic;
    public bool entered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BackgroundMusic.Stop();
        CombatMusic.Play();
        entered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        CombatMusic.Stop();
        BackgroundMusic.Play();
        entered = false;
    }
}

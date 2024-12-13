using UnityEngine;

public class GateTrigger : MonoBehaviour
{

    public Transform player;

    Collider m_ObjectCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_ObjectCollider.isTrigger) {
            player.position = new Vector3(980.2f, 15f, 1280f);
        }
        
    }
}

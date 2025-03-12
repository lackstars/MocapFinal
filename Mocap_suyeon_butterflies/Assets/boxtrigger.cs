using UnityEngine;

public class boxtrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }

}


using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    
    public PlayerController playerController;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        
        playerController.ChangeHealth(1);
        Destroy(gameObject);
    }
}

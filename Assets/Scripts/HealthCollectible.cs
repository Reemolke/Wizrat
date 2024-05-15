
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    
    private PlayerController playerController;
    void Start()
    {
       playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player"){
            playerController.ChangeHealth(1);
            Destroy(gameObject);
        }
        
    }
}

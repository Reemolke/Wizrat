
using UnityEngine;


public class CoinCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            ScoreManager.scoreManager.raiseScore(1);
                
            Destroy(gameObject);
        }
        
        
        


    }
}

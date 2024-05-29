
using UnityEngine;


public class CoinCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip collectSound ; // Asignar en el Inspector o puedes usar el AudioSource directamente

    private AudioSource audioSource;
    
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            ScoreManager.scoreManager.raiseScore(1);
            PlayCollectSound();
            Destroy(gameObject);
        }
        

    }
    void PlayCollectSound()
    {
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        else if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}

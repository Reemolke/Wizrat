using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float dir;
    private Animator anim;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Awake()
    {
      anim = GetComponent<Animator>();
      boxCollider = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * dir;
        transform.Translate(movementSpeed,-0.001f,0);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        hit = true;
        boxCollider.enabled = false;
        
    }
    public void SetDirection(float direction){
        dir = direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction){
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y,transform.localScale.z);

    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}

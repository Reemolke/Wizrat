using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    BoxCollider2D boxCollider;
    Rigidbody2D body;
    Animator anim;
    bool damage = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.idle){
            anim.enabled = false;
        }else{
            anim.enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            DamagePlayer();
        }
    }
    void DamagePlayer(){
        player.GetComponent<PlayerController>().ChangeHealth(-1);

    }
    void CanDamage(){
        damage = !damage;
        boxCollider.enabled = damage;
        
    }
}

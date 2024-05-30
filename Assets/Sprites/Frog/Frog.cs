using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    
    [SerializeField]private float chaseSpeed = 5f;
    [SerializeField]private int health = 3; // Vida del enemigo
    private Rigidbody2D body;
    private bool jumping;
    private Animator anim;
    [SerializeField] private GameObject player;
    [SerializeField] private float detectionRadius = 10f;
    
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    

    // Update is called once per frame
    void Update()
    {
        
        // Si el enemigo está en estado de persecución, perseguir al jugador
        if (IsOnRadius() && jumping == false && !PlayerController.idle)
        {   
            body.simulated = true;
            anim.enabled = true;
            anim.SetTrigger("Jump");
            ChasePlayer();
        }else if(PlayerController.idle){
            body.simulated = false;
            anim.enabled = false;
        }else{
            body.simulated = true;
            anim.enabled = true;
        }
        if(transform.position.y < -20){
            Die();
        }
        
    }
    
    
    void ChasePlayer()
    {
       
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        Vector3 direccion = player.transform.position-transform.position;
        if (direccion.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direccion.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        
        ForceApply(4,6,-transform.localScale.x);
        jumping = true;
        
        
    }
    bool IsOnRadius(){
        if(player == null){
            return false;
        }else{
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= detectionRadius;
        }
    }
    // Cambiar la dirección del enemigo
    

    // Detectar colisión con el ataque del jugador
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si la colisión es con el ataque del jugador
        
        if (other.CompareTag("Attack"))
        {
            // Restar vida al enemigo
            ForceApply(8,2,-player.transform.localScale.x);
            anim.SetTrigger("Hurt");
            TakeDamage();
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            ForceApply(4,2,-player.transform.localScale.x);
            player.GetComponent<PlayerController>().ChangeHealth(-2);
            TakeDamage();
        }else if(other.gameObject.CompareTag("Ground")){
            jumping=false;
        }
    }

    // Función para restar vida al enemigo
    void TakeDamage()
    {
        health--; // Restar 1 de vida al enemigo

        // Verificar si el enemigo se quedó sin vida
        if (health <= 0)
        {
            // Destruir el enemigo si se quedó sin vida
            Die();
        }
    }
    void Die(){
        ScoreManager.scoreManager.raiseScore(10);
        Destroy(gameObject);
    }
    public void ForceApply(int force, int forceUp,float dir){

        body.velocity = new Vector2(-Mathf.Sign(dir) * force,forceUp);
    }
}


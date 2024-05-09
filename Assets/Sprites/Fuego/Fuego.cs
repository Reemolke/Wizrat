using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    [SerializeField]private float speed = 3f; // Velocidad de movimiento del enemigo
    [SerializeField]private float chaseSpeed = 3f;
    [SerializeField]private int health = 3; // Vida del enemigo
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    private int direction = 1; // Dirección inicial del enemigo (1: derecha, -1: izquierda)
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    

    // Update is called once per frame
    void Update()
    {
        
        // Si el enemigo está en estado de persecución, perseguir al jugador
        if (IsOnRadius())
        {
            ChasePlayer();
        }
        
        //anim.SetBool("Chasing",IsOnRadius());
    }
    
    void ChangeDirection(){
        direction *= -1;
    }
    void ChasePlayer()
    {
        // Calcula la dirección hacia el jugador
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Aplica la velocidad dirigida hacia el jugador
        body.velocity = directionToPlayer * chaseSpeed;

        // Voltea la escala del sprite según la dirección
        if (directionToPlayer.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionToPlayer.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    bool IsOnRadius(){
        if(player == null){
            return false;
        }else{
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
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
            ForceApply(4,1,-player.localScale.x);
            //anim.SetTrigger("hit");
            TakeDamage();
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            ForceApply(8,8,-player.localScale.x);
        }else if(other.gameObject.CompareTag("Obstacle")){
            ChangeDirection();
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
            Destroy(gameObject);
        }
    }
    public void ForceApply(int force, int forceUp,float dir){

        body.velocity = new Vector2(-Mathf.Sign(dir) * force,forceUp);
    }
}


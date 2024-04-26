using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float speed = 3f; // Velocidad de movimiento del enemigo
    [SerializeField]private float chaseSpeed = 4f;
    [SerializeField]private int health = 3; // Vida del enemigo
    private Rigidbody2D body;
    
    private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    private int direction = 1; // Dirección inicial del enemigo (1: derecha, -1: izquierda)
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    

    // Update is called once per frame
    void Update()
    {
        
        // Si el enemigo está en estado de persecución, perseguir al jugador
        if (isOnRadius())
        {
            ChasePlayer();
        }
        else // Si no, realizar la patrulla normal
        {
            Patrol();
        }
    }
    void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }
    void ChasePlayer()
    {
       
        
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            
        
        
    }
    bool isOnRadius(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        return distanceToPlayer <= detectionRadius;
    }
    // Cambiar la dirección del enemigo
    void ChangeDirection()
    {
        direction *= -1; // Invertir la dirección (de derecha a izquierda o viceversa)
    }

    // Detectar colisión con el ataque del jugador
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si la colisión es con el ataque del jugador
        
        if (other.CompareTag("Attack"))
        {
            // Restar vida al enemigo
            ForceApply(10,2,-player.localScale.x);
            TakeDamage();
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            ForceApply(10,2,-player.localScale.x);
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


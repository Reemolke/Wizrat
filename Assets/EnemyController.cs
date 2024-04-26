using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float speed = 5f; // Velocidad de movimiento del enemigo
    [SerializeField]private float leftLimit = -20f; // Límite izquierdo de movimiento
    [SerializeField]private float rightLimit = 20f; // Límite derecho de movimiento
    [SerializeField]private float chaseSpeed = 6f;
    [SerializeField]private int health = 3; // Vida del enemigo
    private Rigidbody2D body;
    private bool isChasing = false; // Estado de persecución del enemigo
    private Transform player;
    [SerializeField] private float detectionRadius = 5f;
    private int direction = 1; // Dirección inicial del enemigo (1: derecha, -1: izquierda)
    void Awake(){
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player);
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
       
        if(isOnRadius()){
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            
        }
        else // Si el jugador está fuera del radio de detección, dejar de perseguirlo
        {
            isChasing = false;
        }
    }
    bool isOnRadius(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectionRadius){
            isChasing = true;
        }else{
            isChasing = false;
        }
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
            body.velocity = new Vector2(-Mathf.Sign(-transform.localScale.x) * 10,2);
            TakeDamage();
        }
        isChasing = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false; // Desactivar el estado de persecución
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
}


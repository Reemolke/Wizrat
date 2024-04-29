using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] public int maxHealth = 5;
        [SerializeField] int currentHealth;
        private Animator animator;
        private Rigidbody2D body;
        private BoxCollider2D boxCollider;
        private float walljumpCooldown;
        private bool block;
        public bool grounded;
        public float horizontalInput;
        private void Awake(){
            speed = 5f;
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            currentHealth = maxHealth;
        }
        
        /* private void FixedUpdate(){
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetFloat("Dir X", -1);
                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetFloat("Dir X", 1);
                
            }
            Vector2 movement = new Vector2(dir.x, dir.y).normalized;
            Vector2 position = transform.position;

            // Ajustar la velocidad para que sea independiente del framerate
            float speed = 4f;
            position.x = position.x + speed * movement.x * Time.deltaTime;
            position.y = position.y + speed * movement.y * Time.deltaTime;

            transform.position = position;
            animator.SetFloat("Speed", movement.magnitude);
            animator.SetBool("Grounded",grounded);
        } */

        
        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");

            //Flip sprite
            if(horizontalInput > 0.01f){
                transform.localScale = Vector3.one;
            }else if(horizontalInput < -0.01f){
                transform.localScale = new Vector3(-1,1,1);
            }

            if(walljumpCooldown > 0.2f && !block){
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                if(onWall() && !isGrounded(groundLayer)){
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                    grounded = true;
                }else{
                    body.gravityScale = 1;
                }
                if(Input.GetKeyDown(KeyCode.Space) && !block){
                    Jump();
                }
                
            }else{
                walljumpCooldown += Time.deltaTime;
            }
            if(Input.GetKeyDown(KeyCode.E) && block == false && isGrounded(groundLayer)){
                
                block = true;
            }else if(Input.GetKeyUp(KeyCode.E)){
                
                block = false;
            }
            
            animator.SetBool("Running",horizontalInput != 0);
            animator.SetBool("Grounded", grounded);
            animator.SetBool("shield",block);
            
        }
        private void Jump(){
            
            if(isGrounded(groundLayer) || isGrounded(obstacleLayer)){
                
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                body.gravityScale = 1;
                    
                grounded=false;
                animator.SetTrigger("jump");
                
                
            }else if(onWall()){
                if(horizontalInput == 0){
                    ForceApply(10,4);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
                }else{
                    ForceApply(6,6);
                }
                walljumpCooldown = 0;
                grounded = false;
                animator.SetTrigger("jump");
            }
            
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isGrounded(obstacleLayer) || isGrounded(groundLayer))
            {
                grounded = true;
                
            }
            if(collision.gameObject.tag == "Gems"){
                body.velocity = new Vector2(body.velocity.x, jumpPower);    
            }
            if(collision.gameObject.tag == "Enemy"){
                if(block){
                    block= false;
                }else{
                    ChangeHealth(-1);
                    ForceApply(10,4);
                }
                
            }
        }
        private bool isGrounded(LayerMask mask){
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f, mask);
            return raycastHit2D.collider != null;
        }

        private bool onWall(){
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f, obstacleLayer);
            return raycastHit2D.collider != null;
        }

        public bool canAttack(){
            return !onWall() && !block;
        }

        public void ChangeHealth (int amount)
        {
            
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            if(amount < 0){
                animator.SetTrigger("hit");
            }
            
        }
        public void ForceApply(int force, int forceUp){
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * force,forceUp);
        }
    }


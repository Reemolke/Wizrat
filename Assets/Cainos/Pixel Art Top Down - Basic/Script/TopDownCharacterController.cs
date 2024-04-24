using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask obstacleLayer;
        private Animator animator;
        private Rigidbody2D body;
        private BoxCollider2D boxCollider;
        private float walljumpCooldown;
        public bool grounded;
        public float horizontalInput;
        private void Awake(){
            speed = 5f;
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
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

            if(walljumpCooldown > 0.2f){
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                if(onWall() && !isGrounded()){
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }else{
                    body.gravityScale = 1;
                }
                if(Input.GetKey(KeyCode.Space)){
                    Jump();
                }
                
            }else{
                walljumpCooldown += Time.deltaTime;
            }
                

            animator.SetBool("Running",horizontalInput != 0);
            animator.SetBool("Grounded", grounded);

            
        }
        private void Jump(){
            
            if(isGrounded()){
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                body.gravityScale = 1;
                grounded = false;
            }else if(onWall() && !isGrounded()){
                
                if(horizontalInput == 0){
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10,4);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
                    
                }else{
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6,6);
                    
                }
                grounded = false;
                walljumpCooldown = 0;
                
            }
            
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                grounded = true;
                
            }else if(collision.gameObject.tag == "Obstacle"){
                grounded = true; 
            }
        }
        private bool isGrounded(){
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f, groundLayer);
            return raycastHit2D.collider != null;
        }

        private bool onWall(){
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f, obstacleLayer);
            return raycastHit2D.collider != null;
        }
    }
}

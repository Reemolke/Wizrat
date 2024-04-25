using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacl : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    private Animator anim;
    private PlayerController playerMovement;
    [SerializeField]private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] proyectiles;
     // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack(){
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        proyectiles[FindProyectile()].transform.position = firePoint.position;
        proyectiles[FindProyectile()].GetComponent<Proyectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindProyectile(){
        for(int i = 0; i<proyectiles.Length; i++){
            if(!proyectiles[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
}

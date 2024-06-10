using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCooldown : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isCooldown = false;
    public Image imageCooldown;

    private float cooldownTimer = 0.0f;
    private float cooldownTime = 10.0f;
    void Start()
    {
        imageCooldown.fillAmount =0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ApplyCooldown(){
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0.0f){
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }else{
            imageCooldown.fillAmount = cooldownTimer/cooldownTime;
        }
    }
    public void UseSpell(){
        if(isCooldown){
            
        }
        isCooldown = true;
        cooldownTimer = cooldownTime;
        
    }
}

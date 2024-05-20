using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthMeasure : MonoBehaviour
{   
    public GameObject player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        health= player.GetComponent<PlayerController>().currentHealth;
        foreach(Image img in hearts){
            img.sprite = emptyHeart;
        }

        for(int i =0 ;i<health;i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}

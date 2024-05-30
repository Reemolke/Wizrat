using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI texto;
    UIManager _ui;
    // Start is called before the first frame update
    void Start()
    {
       
        if(LevelManager.instance == null){
            instance = this;
            UIManager _ui = GetComponent<UIManager>();
        }else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if(_ui != null){
            _ui.ToggleDeathPanel();
        }
    }

    public void Win(){
        

        UIManager _ui = GetComponent<UIManager>();
        if(_ui != null){
            _ui.ToggleWinPanel();
        }
        texto.text = "You win! Total score: " + ScoreManager.totalScore.ToString();
    }

    public void Menu(){
        UIManager _ui = GetComponent<UIManager>();
        if(_ui != null){
            _ui.ToggleMenu();
        }
    }
}

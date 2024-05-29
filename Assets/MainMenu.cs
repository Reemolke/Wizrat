
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    GameObject startMenu;
    GameObject levelPicker;
    Text text;
    void Awake(){
        startMenu = GameObject.Find("StartMenu");
        levelPicker = GameObject.Find("LevelPicker");
        levelPicker.SetActive(false);
       
        text=  GameObject.Find("Text").GetComponent<Text>();
        
        
        text.text = ScoreManager.totalScore.ToString();
        
    }
    public void StartGame(){

        startMenu.SetActive(false);
        levelPicker.SetActive(true);
        
    }
    public void StartLevel(string levelName){
        SceneManager.LoadSceneAsync(levelName);
    }
}

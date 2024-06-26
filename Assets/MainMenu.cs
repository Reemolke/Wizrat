
using TMPro;
using TMPro.Examples;
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

    
    public TextMeshProUGUI text;
    void Awake(){
        startMenu = GameObject.Find("StartMenu");
        levelPicker = GameObject.Find("LevelPicker");
        levelPicker.SetActive(false);
       
        
        
        
        text.text = ScoreManager.totalScore.ToString();
        ScoreManager.totalScore = 0;
        
    }
    public void StartGame(){

        startMenu.SetActive(false);
        levelPicker.SetActive(true);
        
    }
    public void StartLevel(string levelName){
        SceneManager.LoadSceneAsync(levelName);
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void  Back(){

        startMenu.SetActive(true);
        levelPicker.SetActive(false);
        
    }
}


using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    GameObject startMenu;
    GameObject levelPicker;
    void Awake(){
        startMenu = GameObject.Find("StartMenu");
        levelPicker = GameObject.Find("LevelPicker");
        levelPicker.SetActive(false);
    }
    public void StartGame(){

        startMenu.SetActive(false);
        levelPicker.SetActive(true);
        
    }
    public void StartLevel(string levelName){
        SceneManager.LoadSceneAsync(levelName);
    }
}

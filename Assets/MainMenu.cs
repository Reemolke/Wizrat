
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Wizrat"));
        SceneManager.LoadSceneAsync(0);
    }
}

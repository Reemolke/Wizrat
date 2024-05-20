using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReloadCurrentScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSceneByName(string name){
        if (name != null){
            SceneManager.LoadScene(name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.instance == null){
            instance = this;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject menuPanel;

    public void ToggleDeathPanel(){
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
    public void ToggleWinPanel(){
        winPanel.SetActive(!winPanel.activeSelf);
    }
    public void ToggleMenu(){
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}

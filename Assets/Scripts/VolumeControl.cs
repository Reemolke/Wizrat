using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volumen",0.5f);
        AudioListener.volume = slider.value;
    }
    public void Changeslider(float valor){
        sliderValue= valor;
        PlayerPrefs.SetFloat("Volumen",sliderValue);
        AudioListener.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

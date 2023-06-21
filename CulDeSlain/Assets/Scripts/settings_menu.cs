using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;

public class settings_menu : MonoBehaviour
{

    public GameManager gameManager ;
    public TMP_Dropdown dropDown ;

    void Start(){
        dropDown.value = gameManager.PlayerSettings.qualityIndex ;
    }
    public void SetVolume(float volume){
        Debug.Log(volume) ;
    }

    public void SetSenitivity(float sen){
        gameManager.changeSenitivity(sen) ;
    }

    public void setQuality(int qualityIndex){
        
        gameManager.changeQuality(qualityIndex) ;
        
    }

    public void setFov(float fov){
        gameManager.changeFov(fov) ;
    }
}

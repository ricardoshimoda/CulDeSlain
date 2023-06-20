using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class settings_menu : MonoBehaviour
{

    public GameManager gameManager ;

    
    public void SetVolume(float volume){
        Debug.Log(volume) ;
    }

    public void SetSenitivity(float sen){
        gameManager.changeSenitivity(sen) ;
    }
}

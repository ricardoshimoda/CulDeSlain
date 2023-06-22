using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void playGame(){
        Invoke(nameof(Resumeloader), 0.15f) ;
        
        
    }
    
    public void quitGame(){
        Application.Quit() ;
        
    }


    void Resumeloader(){
        SceneManager.LoadScene("HUD") ;
    }
    
}

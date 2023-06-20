using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame(){
        Debug.Log("START");
        
        
    }
    public void settings(){
        Debug.Log("SETTINGS");
        SceneManager.LoadScene("SettingsMenu");
        
    }
    public void backToMenu(){
        Debug.Log("BACKTOMENU");
        SceneManager.LoadScene("ArjunShaffan_Mainmenu");
        
    }
    public void quitGame(){
        Debug.Log("QUIT");
        
    }
   
}

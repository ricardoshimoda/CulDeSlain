using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade_to_Credit : MonoBehaviour
{

    public GameObject completeLevelUI;

    // Start is called before the first frame update
    public void Load_Cred()
    {
        Debug.Log("WORKING");
        completeLevelUI.SetActive(true);
        //SceneManager.LoadScene("Credit_Scene");
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
   public GameObject WinScreenFinish;

    // Start is called before the first frame update
    public void cred_transitioned()
    {
        WinScreenFinish.SetActive(false);
        //SceneManager.LoadScene("Credit_Scene");
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyEnter : MonoBehaviour
{
    public KeyCode key;
    public Button targetButton;

    void Update()
    {
       if (Input.GetKeyDown(key))
        {
            targetButton.onClick.Invoke();
        }
    }
}
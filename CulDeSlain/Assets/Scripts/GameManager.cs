using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings {
    
    [Header("Save File Details")] 
    public string CurrentDate;

    [Header("Controls")] 
    
    [Header("Camera Settings")]
    public float PlayerFOV = 80f;
    
    [Header("Mouse Settings")]
    public float MouseSensitivity = 0.2f;
    public float ViewBobbingIntensitiy = 1f;

}

public class GameManager : SingletonPersistent<GameManager> {

    public PlayerSettings PlayerSettings = new PlayerSettings();

    protected override void Awake() {
        base.Awake();
        
        LoadPlayerSettings();
    }

    private void SavePlayerSettings() {
        //Getting Data
        PlayerSettings.CurrentDate = DateTime.Now.ToString("f");

        // Saving Data
        if (!File.Exists(Application.dataPath + "/player.sav"))
            File.Create(Application.dataPath + "/player.sav");

        try { File.WriteAllText(Application.dataPath + "/player.sav", JsonUtility.ToJson(PlayerSettings, true)); }
        catch (Exception e) {
            if (e.GetType() == typeof(IOException)) {
                StartCoroutine("LoadingDelay");
            }
        }
        
    }

    private void LoadPlayerSettings() {
        //Getting Save File
        if (File.Exists(Application.dataPath + "/player.sav"))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/player.sav"), PlayerSettings);
        else SavePlayerSettings();
    }

    private void OnDisable() {
        SavePlayerSettings();
    }

    IEnumerable LoadingDelay() {
        yield return new WaitForSecondsRealtime(0.5f);
        SavePlayerSettings();
    }
}

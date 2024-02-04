using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static string playerName;
    public static int highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SavePlayerData
    {
        public int PlayerHighScore;
    }

    public void SaveData()
    {
        SavePlayerData playerData = new SavePlayerData();
        playerData.PlayerHighScore = highScore;

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            SavePlayerData playerData = JsonUtility.FromJson<SavePlayerData>(jsonText);
            highScore = playerData.PlayerHighScore;
        }
    }
}

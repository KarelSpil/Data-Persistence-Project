using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public static StartManager Instance;
    public SaveData userData;
    public Text bestScoreText;
    public string userName;
    
    private void Awake()
    {   
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        userData = new SaveData();
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHigh();
    }

    [System.Serializable]
    public class SaveData
    {
        public string userName;
        public int hightScore;
    }

    public void SaveHight()
    {
        SaveData data = new SaveData();
        data.userName = userData.userName;
        data.hightScore = userData.hightScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHigh()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userData.userName = data.userName;
            userData.hightScore = data.hightScore;
        }
        else
        {
            userData.userName = userName;
            userData.hightScore = 0;
            SaveHight();
        }

        bestScoreText.text = $"Best Score:\n{userData.userName}: {userData.hightScore.ToString()}";
    }
}

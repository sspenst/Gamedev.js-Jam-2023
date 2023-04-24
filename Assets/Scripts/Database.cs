using System.IO;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database Instance { get; private set; }

    public int HighScore = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDatabase();
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
    }

    public void SaveDatabase()
    {
        File.WriteAllText(
            Application.persistentDataPath + "/highscore.json",
            JsonUtility.ToJson(new SaveData
            {
                HighScore = HighScore
            })
        );
    }

    public void LoadDatabase()
    {
        string path = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));

            HighScore = data.HighScore;
        }
    }
}

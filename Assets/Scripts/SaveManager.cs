using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public SaveData activeSave;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadGame()
    {
        string dataPath = Application.persistentDataPath;
        if(File.Exists(dataPath + "/Save Game.data"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/Save Game.data", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Data Loaded");
        }
    }
    public void SaveGame()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/Save Game.data", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("Data Saved");
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

[System.Serializable]
public class SaveData
{
    public float maxHealth, maxMagic;
    public int currentCoin;
    public float healingSpeed, magicRefillSpeed;
    public int attackDamage;
}

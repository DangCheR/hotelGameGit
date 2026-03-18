
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
class JSONsave:MonoBehaviour
{
    PlayerDataListWrapper playerDataListWrapper;
    public string jsonPath = "JSON/玩家数据.json";

    void Start()
    {
        InitJSON();
    }
    public void InitJSON()
    {
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerDataListWrapper wrapper = JsonUtility.FromJson<PlayerDataListWrapper>(json);
            playerDataListWrapper = wrapper;
        }
        else
        {
            playerDataListWrapper = new PlayerDataListWrapper
            {
                playerDataList = new List<PlayerData>()
                {
                    new PlayerData() { name = "玩家1", level = 10, health = 100 },
                    new PlayerData() { name = "玩家2", level = 20, health = 80 },
                    new PlayerData() { name = "玩家3", level = 15, health = 90 }
                }
            };
            File.Create(path).Close(); // 必须Close()，否则文件会被占用
            SaveData();
        }
    }
    public void SaveData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, jsonPath);
        FileInfo fileInfo = new FileInfo(fullPath);

        string json = JsonUtility.ToJson(playerDataListWrapper, true);
        File.WriteAllText(fullPath, json);
    }
}
[System.Serializable]
class PlayerDataListWrapper
{
    public List<PlayerData> playerDataList;
}
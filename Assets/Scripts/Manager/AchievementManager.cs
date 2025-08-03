using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private string achievementPath = Path.Combine(Application.dataPath + "/Scripts/Achievement/", "AchievementDataJson.json");

    private static AchievementManager instance;
    public static AchievementManager Instance { get { return instance; } }

    public Dictionary<int, AchievementData> AchievementDict = new Dictionary<int, AchievementData>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAchievement();
    }

    private void Start()
    {
        foreach(var achievement in AchievementDict)
        {
            Debug.Log($"Achievement ID: {achievement.Key}, IsCleared: {achievement.Value.IsCleared}");
            Debug.Log($"Achievement Name: {achievement.Value.AchievementName}, Description: {achievement.Value.Description}, StageGoalNumber: {achievement.Value.StageGoalNumber}");
        }
    }
    private void LoadAchievement()
    {
        if(!File.Exists(achievementPath))
        {
            Debug.Log("No");
        }
        else
        {
            
            List<AchievementData> achievementDataList = LoadJsonData<AchievementData>(achievementPath);
            foreach(AchievementData data in achievementDataList)
            {
                if (!AchievementDict.ContainsKey(data.ID))
                {
                    AchievementDict.Add(data.ID, data);
                }
            }
        }
    }
    public List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    public void OnAchievementUnlocked(int achievementID)
    {
        if (AchievementDict.ContainsKey(achievementID))
        {
            AchievementData achievement = AchievementDict[achievementID];
            if (!achievement.IsCleared)
            {
                achievement.IsCleared = true;
                SaveAchievementsToJson();
            }
        }
    }
    public bool[] GetAchievementClear()
    {
        bool[] achievementClear = new bool[AchievementDict.Count];
        int index = 0;
        foreach(var achievement in AchievementDict)
        {
            achievementClear[index] = achievement.Value.IsCleared;
            index++;
        }
        return achievementClear;
    }
    public void SaveAchievementsToJson()
    {
        List<AchievementData> achievementList = new List<AchievementData>(AchievementDict.Values);

        string json = JsonConvert.SerializeObject(achievementList, Formatting.Indented);

        File.WriteAllText(achievementPath, json);
    }
}
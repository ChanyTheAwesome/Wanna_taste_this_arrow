using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private string achievementPath = Path.Combine(Application.streamingAssetsPath, "AchievementDataJson.json");
    private string achivementPersistentPath;
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
        achivementPersistentPath = Path.Combine(Application.persistentDataPath, "AchievementDataJson.json");
        LoadAchievement();
    }

    private void LoadAchievement()
    {
        if (!File.Exists(achivementPersistentPath))
        {
            if (!File.Exists(achievementPath))
            {
                Debug.LogError("Achievement data file not found at:" + achievementPath);
            }
            else
            {
                List<AchievementData> achievementDataList = LoadJsonData<AchievementData>(achievementPath);
                foreach (AchievementData data in achievementDataList)
                {
                    if (!AchievementDict.ContainsKey(data.ID))
                    {
                        AchievementDict.Add(data.ID, data);
                    }
                }
            }
        }
        else
        {
            List<AchievementData> achievementDataList = LoadJsonData<AchievementData>(achivementPersistentPath);
            foreach (AchievementData data in achievementDataList)
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

    public void OnDungeonClear(int dungeonNumber)   // 스테이지 클리어시 호출하기
    {
        Debug.Log(DungeonManager.Instance.CurrentDungeonID);
        foreach(var achievement in AchievementDict.Values)
        {
            if (achievement.DungeonGoalNumber == dungeonNumber && !achievement.IsCleared)
            {
                achievement.IsCleared = true;
                SaveAchievementsToJson();
                UIManager.Instance.SetAchievement(achievement.AchievementName, achievement.Description);
            }
        }
    }
    public bool[] GetAchievementClear() // 도전과제 깼는지 안깼는지를 가져오는 친구, 배열만 가져오는 역할
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

        File.WriteAllText(achivementPersistentPath, json);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData
{
    public int ID { get; set; }
    public string AchievementName { get; set; }
    public string Description { get; set; }
    public string AchievementType { get; set; }
    public IAchievable Achievable { get; set; }
    public bool IsAchieved =>Achievable.IsAchieved;
    public bool IsCleared { get; set; }
}
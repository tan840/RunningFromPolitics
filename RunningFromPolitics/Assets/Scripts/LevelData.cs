using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    [HideInInspector]
    public string Name = "Level";
    public Platform[] LevelPlatforms;
}

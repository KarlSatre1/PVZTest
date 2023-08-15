using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelData : ScriptableObject
{
    public list<LevelItem> levelDataList = new List<LevelItem>();
}
[System.Serializable]
public class LevelItem
{
    public int ID;

    public int DungeonsID;

    public int ProgressID;
    public int CreateTime;
    public int ZombieType;
    public int BornPos;
    

}

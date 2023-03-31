using System;
using UnityEngine;

[Serializable]
public class BuildingData
{
    public int id;
    public Vector3 position;
    public bool isInProduction;
    public int productionID;
    public EndTime endTime;
}


[Serializable]
public class EndTime
{
    public int years;
    public int months;
    public int days;
    public int hours;
    public int minutes;
    public int seconds;
}
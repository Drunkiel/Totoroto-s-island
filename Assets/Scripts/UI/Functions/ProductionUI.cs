using System;
using TMPro;
using UnityEngine;

public class ProductionUI : MonoBehaviour
{
    public DateTime endTime;
    public int productionID;
    public bool isProductionEnded;

    public TMP_Text timeText;

    private OpenClose _openClose;

    void Start()
    {
        _openClose = GetComponent<OpenClose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_openClose.isOpen)
        {
            timeText.text = CalculateTime();
            isProductionEnded = CheckIfProductionEnds();
        }
    }

    private string CalculateTime()
    {
        var newTime = endTime.Subtract(DateTime.Now);

        int daysLeft = newTime.Days;
        int hoursLeft = newTime.Hours;
        int minutesLeft = newTime.Minutes;
        int secondsLeft = newTime.Seconds;

        return (hoursLeft + daysLeft * 24) + ":" + minutesLeft + ":" + secondsLeft;
    }

    private bool CheckIfProductionEnds()
    {
        if (endTime.Subtract(DateTime.Now) < TimeSpan.Zero && endTime != new DateTime()) return true;
        else return false;
    }
}

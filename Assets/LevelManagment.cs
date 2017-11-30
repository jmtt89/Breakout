using System;
using UnityEngine;

public static class LevelManager
{
    private static DateTime _startBonusTime;
    public static int numInitialBlocks = 0;

    private static int _points = 0;
    public static int Points
    {
        get
        {
            return _points;
        }

        set
        {
            if(DateTime.Now - _startBonusTime <= TimeSpan.FromSeconds(15))
            {
                _points = value * 2;
            }
            else
            {
                _points = value;
            }
        }
    }

    public static void StartBonus()
    {
        _startBonusTime = System.DateTime.Now;
    }

    public static bool InBonusTime()
    {
        return DateTime.Now - _startBonusTime <= TimeSpan.FromSeconds(15);
    }

    private static int _powerBomb;

    public static int PowerBomb { get { return _powerBomb; } }

    

    public static void UsePowerBomb()
    {
        if(_powerBomb > 0)
        {
            _powerBomb--;
        }

    }

    public static void AddPowerBomb()
    {
        if(_powerBomb < 3)
        {
            _powerBomb++;
        }
    }

}
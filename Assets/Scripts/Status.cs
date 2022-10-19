using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{

    public int duration;
    public string StatusName;
    public int HPChanges;
    public bool isStarted = false;

    public Status(int duration, string StatusName, int HPChanges)
    {
        this.duration = duration;
        this.StatusName = StatusName;
        this.HPChanges = HPChanges;
    }

    public void StartStatus()
    {
        isStarted = true;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Event
{
    public int Id;
    public string Name;
    public int Count;
    public string Description;
    public int PlayOtherTime;

    public Event(int id, string name, int count, int playedLater, string desc)
    {
        Id = id;
        Name = name;
        Count = count;
        PlayOtherTime = playedLater;
        Description = desc;
    }

    public override string ToString()
    {
        return Name + ": " + Description;
    }
}

[Serializable]
// Helper class for JSON parsing
public class Events
{
    public Event[] events;
}

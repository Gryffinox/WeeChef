using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCard : MonoBehaviour
{
    public int EventId;
    public string EventName;
    public int EventCardCount;
    public int EventCanBePlayedLater = 0;
    public string EventDescription;

    public EventCard()
    {

    }

    public EventCard(int id, string name, int count, int playedLater, string desc)
    {
        EventId = id;
        EventName = name;
        EventCardCount = count;
        EventCanBePlayedLater = playedLater;
        EventDescription = desc;
    }

    public override string ToString()
    {
        return EventName + ": " + EventDescription;
    }
}

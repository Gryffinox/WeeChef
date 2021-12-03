using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventList : MonoBehaviour
{
    [SerializeField] GameObject DrawnCardPanel;
    [SerializeField] GameObject CardInPlayerPanel;
    [SerializeField] Button showCardBtn;
    [SerializeField] Button putAwayCardBtn;
    [SerializeField] TextAsset EventsTextFile;
    private List<EventCard> EventsDeck;
    private EventCard drawnCard;
    private Text drawnCardText;

    void Start()
    {
        drawnCardText = DrawnCardPanel.GetComponentInChildren<Text>();
        DrawnCardPanel.SetActive(false);
        putAwayCardBtn.gameObject.SetActive(false);
        EventsDeck = new List<EventCard>();
        LoadEvents();
    }

    // Load Events from file
    private void LoadEvents()
    {
        Events events = JsonUtility.FromJson<Events>(EventsTextFile.text);
        foreach(Event e in events.events)
        {
            EventCard anEventcard = new EventCard(
                e.Id,
                e.Name,
                e.Count,
                e.PlayOtherTime,
                e.Description);
            EventsDeck.Add(anEventcard);
        }
        Shuffle(EventsDeck);
    }

    // Displays to the player the drawn event card
    public void DisplayDrawnEventCard()
    {
        drawnCard = DrawEventCard();
        // Enable panel to display the card
        DrawnCardPanel.SetActive(true);
        // Change panel text
        drawnCardText.text = drawnCard.ToString();
        // Hide button to draw card
        showCardBtn.gameObject.SetActive(false);
        // Show button to put away card
        putAwayCardBtn.gameObject.SetActive(true);
    }

    // Puts the drawn card in the player menu
    public void PutAwayEventCard()
    {
        // Display the card
        CardInPlayerPanel.GetComponentInChildren<Text>().text = drawnCard.ToString();
        // Hide panel
        DrawnCardPanel.SetActive(false);
        // Display Draw Card Button again
        putAwayCardBtn.gameObject.SetActive(false);
        showCardBtn.gameObject.SetActive(true);
    }

    // Returns the first card of the deck
    private EventCard DrawEventCard()
    {
        if (EventsDeck.Count == 0)
            LoadEvents();

        EventCard drawnCard = EventsDeck[0];
        EventsDeck.RemoveAt(0);
        return drawnCard;
    }

    // Shuffle the EventsDeck
    private void Shuffle(List<EventCard> eventDeck)
    {
        int i = eventDeck.Count;
        while (i > 1)
        {
            i--;
            int s = Random.Range(0, i + 1);
            EventCard swap = eventDeck[s];
            eventDeck[s] = eventDeck[i];
            eventDeck[i] = swap;
        }
    }


}

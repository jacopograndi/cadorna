using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventEngine : MonoBehaviour {

    Logger logger;
    List<CadEvent> cevents = new List<CadEvent>();

    public int month;
    string[] monthToString = new string[12] {
        "January", "February", "March", "April", 
        "May", "June", "July", "August", 
        "September", "October", "November", "December"
    };
    public int year;
    public TMP_Text date;

    public GameObject panelPrefab;
    Canvas canvas;

    void Start() {
        logger = FindObjectOfType<Logger>();

        CadEvent cevent = new CadEvent();
        LoadEvents.SaveData(cevent);
        cevents = LoadEvents.LoadData();
        logger.Print("Loaded events: " + cevents.Count);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    void Update() {
        date.text = monthToString[month] + " " + year;
    }

    public void EndMonth () {
        month++;
        if (month >= 12) {
            month = 0;
            year++;
        }

        ScriptedEvent();
        RandomEvent();
    }

    public void Popup (CadEvent e) {
        var obj = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(canvas.transform);
        obj.transform.position = Vector3.zero;
        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
        obj.GetComponent<EventPopup>().Construct(e);
    }

    public void ScriptedEvent () {
        foreach(CadEvent e in cevents) {
            if (e.month == month && e.year == year) {
                Popup(e);
            }
        }
    }

    public void RandomEvent () {
        int amt = 1;
        if (Random.Range(0, 10) == 0) amt = 2;
        List<CadEvent> avail = new List<CadEvent>();
        foreach (CadEvent e in cevents) {
            if (e.eventImportance == 0) {
                avail.Add(e);
            }
        }
        for (int i=0; i<amt; i++) {
            if (avail.Count == 0) break;
            CadEvent rand = avail[Random.Range(0, avail.Count)];
            Popup(rand);
            avail.Remove(rand);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EventEngine : MonoBehaviour {
    public class EventRecord {
        public CadEvent cevent;
        public int selection;
        public int month, year;
    }
    List<EventRecord> records = new List<EventRecord>();

    string[] monthToSeason = new string[12] { 
        "winter", "winter", "spring", "spring",
        "spring", "summer", "summer", "summer",
        "fall", "fall", "fall", "winter",
    };

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

    public CadState state;

    void Start() {
        state = FindObjectOfType<CadState>();

        logger = FindObjectOfType<Logger>();

        CadEvent cevent = new CadEvent();
        cevent.ConstructTemplate();
        LoadEvents.SaveData(cevent);
        cevents = LoadEvents.LoadData();
        foreach (CadEvent e in cevents) {
            print(e.PrettyPrint());
        }
        logger.Print("Loaded events: " + cevents.Count);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    void Update() {
        date.text = monthToString[month] + " " + year;
    }

    public void EventSelected (CadEvent cevent, int sel) {
        EventRecord rec = new EventRecord();
        rec.cevent = cevent;
        rec.selection = sel;
        rec.year = year; rec.month = month;
        records.Add(rec);
    }

    public void EndMonth () {
        EventPopup[] popups = FindObjectsOfType<EventPopup>();
        if (popups.Length > 0) return;

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
            if (CheckEventRequirements(e) 
                && e.eventImportance == 1) {
                Popup(e);
            }
        }
    }

    public void RandomEvent () {
        int amt = 1;
        if (Random.Range(0, 10) == 0) amt = 2;
        List<CadEvent> avail = new List<CadEvent>();
        foreach (CadEvent e in cevents) {
            if (CheckEventRequirements(e) 
                && e.eventImportance == 0) {
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

    public bool CheckEventRequirements(CadEvent e) {
        if (e.year != -1 && e.year != year) { return false; }
        if (e.month != -1 && e.month != month) { return false; }
        if (e.season != "not relevant") {
            if (e.season != monthToSeason[month]) {
                return false;
            }
        }
        foreach (CadRequirement req in e.requirements) {
            if (req.armyEfficiencyNeededAtLeast != -1 
                && state.armyEfficiency < req.armyEfficiencyNeededAtLeast) {
                return false;
            }
            if (req.budgetNeededAtLeast != -1
                && state.budget < req.budgetNeededAtLeast) {
                return false;
            }
            if (req.freedomNeededAtLeast != -1
                && state.freedom < req.freedomNeededAtLeast) {
                return false;
            }
            if (req.fronteerNeededAtLeast != -1
                && state.fronteer < req.fronteerNeededAtLeast) {
                return false;
            }
            if (req.popularityNeededAtLeast != -1
                && state.popularity < req.popularityNeededAtLeast) {
                return false;
            }
            if (req.armyEfficiencyNeededAtMost != -1
                && state.armyEfficiency > req.armyEfficiencyNeededAtMost) {
                return false;
            }
            if (req.budgetNeededAtMost != -1
                && state.budget > req.budgetNeededAtMost) {
                return false;
            }
            if (req.freedomNeededAtMost != -1
                && state.freedom > req.freedomNeededAtMost) {
                return false;
            }
            if (req.fronteerNeededAtMost != -1
                && state.fronteer > req.fronteerNeededAtMost) {
                return false;
            }
            if (req.popularityNeededAtMost != -1
                && state.popularity > req.popularityNeededAtMost) {
                return false;
            }
            foreach (CadEventRequired rec in req.requiredEvents) {
                bool ok = false;
                foreach (EventRecord past in records) {
                    if (rec.eventName == past.cevent.name 
                        && (rec.selection != -1 
                            || rec.selection == past.selection)) {
                        ok = true; break;
                    }
                }
                if (!ok) {
                    return false;
                }
            }
        }
        return true;
    }
}

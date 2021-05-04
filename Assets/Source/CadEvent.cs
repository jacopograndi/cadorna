using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CadEventRequired {
    public string eventName;
    public int selection;
}

[System.Serializable]
public class CadRequirement {
    public List<CadEventRequired> requiredEvents = new List<CadEventRequired>();
    public float armyEfficiencyNeededAtLeast;
    public float popularityNeededAtLeast;
    public float freedomNeededAtLeast;
    public float fronteerNeededAtLeast;
    public float budgetNeededAtLeast;
    public float armyEfficiencyNeededAtMost;
    public float popularityNeededAtMost;
    public float freedomNeededAtMost;
    public float fronteerNeededAtMost;
    public float budgetNeededAtMost;

    public CadRequirement() {
        // default values
        armyEfficiencyNeededAtLeast = -1;
        popularityNeededAtLeast = -1;
        freedomNeededAtLeast = -1;
        fronteerNeededAtLeast = -1;
        budgetNeededAtLeast = -1;
        armyEfficiencyNeededAtMost = -1;
        popularityNeededAtMost = -1;
        freedomNeededAtMost = -1;
        fronteerNeededAtMost = -1;
        budgetNeededAtMost = -1;
    }
}

[System.Serializable]
public class CadOption {
    public string text;
    public float armyEfficiencyModify;
    public float popularityModify;
    public float freedomModify;
    public float fronteerModify;
    public float budgetModify;

    public CadOption() {
        // default values
        armyEfficiencyModify = 0;
        popularityModify = 0;
        freedomModify = 0;
        fronteerModify = 0;
        budgetModify = 0;
    }
}

[System.Serializable]
public class CadEvent {
    public int month = -1;
    public int year = -1;
    public string season = "not relevant";
    // 0 is not important, use chance
    // 1 is important, fire at date
    public int eventImportance = 0;
    public float chanceToHappenEveryMonth;
    public string name;
    public string text;

    public List<CadRequirement> requirements = new List<CadRequirement>();
    public List<CadOption> options = new List<CadOption>();

    // event template to save in filesystem
    public CadEvent () {
        month = -1;
        year = -1;
        season = "not relevant";
    }
    public void ConstructTemplate() { 
        month = -1;
        year = -1;
        name = "template example";
        text = "lorem ipsum te dolorem";
        season = "not relevant";
        CadRequirement req = new CadRequirement();
        requirements.Add(req);
        CadRequirement req2 = new CadRequirement();
        CadEventRequired recreq = new CadEventRequired();
        recreq.eventName = "The name of the event, like 'lol'";
        recreq.selection = 0;
        req2.requiredEvents.Add(recreq);
        requirements.Add(req2);

        CadOption opt = new CadOption();
        opt.text = "hey";
        options.Add(opt);

        CadOption opt2 = new CadOption();
        opt2.text = "hey2";
        options.Add(opt2);
    }

    public string PrettyPrint () {
        return JsonUtility.ToJson(this, true);
    }
}
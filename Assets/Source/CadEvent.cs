using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CadRequirement {
    public List<string> requiredEvents = new List<string>();
    public float armyEfficiencyNeeded;
    public float popularityNeeded;
    public float freedomNeeded;
    public float fronteerNeeded;
    public float budgetNeeded;

    public CadRequirement() {
        // default values
        armyEfficiencyNeeded = -1;
        popularityNeeded = -1;
        freedomNeeded = -1;
        fronteerNeeded = -1;
        budgetNeeded = -1;
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
    public int month;
    public int year;
    // 0 is not important, use chance
    // 1 is important, fire at date
    public int eventImportance;
    public float chanceToHappenEveryMonth;
    public string name;
    public string text;

    public List<CadRequirement> requirements = new List<CadRequirement>();
    public List<CadOption> options = new List<CadOption>();

    // event template to save in filesystem
    public CadEvent () {
        month = 1;
        year = 1914;
        name = "template example";
        text = "lorem ipsum te dolorem";
        CadRequirement req = new CadRequirement();
        requirements.Add(req);
        CadRequirement req2 = new CadRequirement();
        req2.requiredEvents.Add("event before 'template example'");
        requirements.Add(req2);

        CadOption opt = new CadOption();
        opt.text = "hey";
        options.Add(opt);

        CadOption opt2 = new CadOption();
        opt2.text = "hey2";
        options.Add(opt2);
    }
}
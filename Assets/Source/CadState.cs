using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadState : MonoBehaviour {

    public float armyEfficiency;
    public float popularity;
    public float freedom;
    public float fronteer;
    public float budget;

    public void ApplyOption (CadOption opt) {
        armyEfficiency += opt.armyEfficiencyModify;
        popularity += opt.popularityModify;
        freedom += opt.freedomModify;
        fronteer += opt.fronteerModify;
        budget += opt.budgetModify;
    }
}

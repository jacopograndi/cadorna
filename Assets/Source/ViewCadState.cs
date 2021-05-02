using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewCadState : MonoBehaviour {

    public TMP_Text armyEfficiency;
    public TMP_Text popularity;
    public TMP_Text freedom;
    public TMP_Text budget;
    public MoveFront fronteer;

    public CadState state;

    void Start() {
        state = FindObjectOfType<CadState>();
    }

    void Update() {
        armyEfficiency.text = "Efficienza dell'Esercito : " + state.armyEfficiency;
        popularity.text = "Popolarità tra le masse : " + state.popularity;
        freedom.text = "Dipendenza dal Governo : " + state.freedom;
    }
}

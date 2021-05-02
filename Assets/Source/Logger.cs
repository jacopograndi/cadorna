using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour {

    TMP_Text log;

    void Start() {
        log = GetComponent<TMP_Text>();
        log.text = "";
    }

    public void Print (object txt) {
        if (!log) {
            log = GetComponent<TMP_Text>();
            log.text = "";
        }
        log.text += txt.ToString() + "\n";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            Color col = log.color;
            col.a = col.a > 0 ? 0 : 1;
            log.color = col;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFront : MonoBehaviour {

    List<LineRenderer> fronts = new List<LineRenderer>();
    LineRenderer frontmove;
    public float front = 1;

    void Start() {
        foreach (Transform line in transform) {
            if (line.name == "FrontMove") {
                frontmove = line.GetComponent<LineRenderer>();
            } else {
                fronts.Add(line.GetComponent<LineRenderer>());
                line.gameObject.SetActive(false);
            }
        }
    }

    void Update() {
        Vector3[] pts = new Vector3[fronts[0].positionCount];
        fronts[0].GetPositions(pts);
        for (int i=0; i<pts.Length; i++) {
            pts[i] = pts[i] * (1 - front) + fronts[1].GetPosition(i) * front;
        }
        frontmove.SetPositions(pts);
    }
}

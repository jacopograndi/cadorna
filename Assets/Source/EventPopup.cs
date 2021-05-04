using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopup : MonoBehaviour {

    public TMP_Text nameLabel;
    public TMP_Text descrLabel;
    public GameObject bottomline;
    public GameObject optionPrefab;

    public CadEvent cevent;

    Logger logger;

    public Vector2 mouseDrag;
    public Vector2 mouseLast;

    void Start() {
        logger = FindObjectOfType<Logger>();
    }

    public void Construct (CadEvent e) {
        cevent = e;
        nameLabel.text = e.name;
        descrLabel.text = e.text;

        bottomline.GetComponent<RectTransform>().anchoredPosition = 
            new Vector3(0, e.options.Count*25+15, 0);
        for (int i=0; i<e.options.Count; i++) {
            Vector3 pos = new Vector3(0, e.options.Count * 25 + 8 - i * 25, 0);
            print(pos);
            var obj = Instantiate(optionPrefab, pos, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            obj.GetComponent<TMP_Text>().text = e.options[i].text;
            int j = i;
            obj.GetComponent<Button>().onClick.AddListener(() => { OptionSelected(j); });
        }
    }

    void OptionSelected (int i) {
        FindObjectOfType<CadState>().ApplyOption(cevent.options[i]);
        FindObjectOfType<EventEngine>().EventSelected(cevent, i);
        logger.Print("Choosen " + i);
        Destroy(gameObject);
    }

    void LateUpdate() {
        /*
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseDelta = mousePos - mouseLast;
        if (nameLabel.gameObject.GetComponent<RectTransform>()
            .rect.Contains(mousePos)) {
            if (Input.GetMouseButtonDown(0)) {
                mouseDrag = mousePos;
            } if (Input.GetMouseButton(0)) {
                Vector3 delta = new Vector3(mouseDelta.x, mouseDelta.y, 0);
                GetComponent<RectTransform>().localPosition += delta;
            }
        }
        mouseLast = mousePos;*/
    }
}

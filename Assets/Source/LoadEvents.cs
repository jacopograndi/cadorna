using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class LoadEvents : MonoBehaviour {

    public static void SaveData(CadEvent cevent) {
        string path = Application.persistentDataPath + "/temp.txt";
        if (!Application.isEditor) {
            path = "Events";
        }
        using (StreamWriter write = new StreamWriter(path)) {
            write.Write(JsonUtility.ToJson(cevent, true));
            write.Close();
        }
    }

    public static List<CadEvent> LoadData() {
        Logger logger = FindObjectOfType<Logger>();

        List<CadEvent> cevents = new List<CadEvent>();
        string path = Application.persistentDataPath;
        if (!Application.isEditor) {
            path = "Events";
        } 
        var result = Directory.EnumerateFiles(path, "*.txt",
            SearchOption.AllDirectories);

        foreach (var file in result) {
            using (StreamReader read = new StreamReader(file)) {
                string raw = read.ReadToEnd();
                read.Close();
                try {
                    CadEvent data = JsonUtility.FromJson<CadEvent>(raw);
                    cevents.Add(data);
                } catch {
                    logger.Print("event " + file + " has a syntax error.");
                }
            } 
        }
        return cevents;
    }
}
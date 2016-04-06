using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {

    public Text timeAmount;
    float truncatedTime;
    public static float roundedTime; 


    // Use this for initialization
    void Start() {
        timeAmount = GameObject.Find("Time Amount").GetComponent<Text>();
        timeAmount.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleTime();
    }

    void handleTime() {
        truncatedTime = (float)(System.Math.Truncate((double)Time.time * 100.0) / 100.0);
        roundedTime = (float)(System.Math.Round((double)truncatedTime, 2));
        timeAmount.text = "" + roundedTime;
    }
}

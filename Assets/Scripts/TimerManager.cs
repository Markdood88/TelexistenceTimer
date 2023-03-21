using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    //Choose TextMeshPro for display
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stopwatchText;

    //Variables for protected access
    protected System.DateTime timeNow;
    protected int hour;
    protected int minute;
    protected int second;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update timeNow for protected access
        timeNow = System.DateTime.UtcNow.ToLocalTime();

        hour = timeNow.Hour;
        minute = timeNow.Minute;
        second = timeNow.Second;

        //update clock
        updateClock(hour, minute, second);
    }

    public void updateClock(int hour, int minute, int second){
        clockText.text = hour.ToString().PadLeft(2,'0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2,'0');
    }
}

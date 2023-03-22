using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class TimerManager : MonoBehaviour
{
    //Choose TextMeshPro for display
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stopwatchText;

    //Variables for protected access
    protected System.DateTime timeNow;
    protected int clockHour;
    protected int clockMinute;
    protected int clockSecond;
    protected float timer;
    protected float timerSaved;
    protected bool timerActive;
    public int timerMaximumMinutes;
    
    // Start is called before the first frame update
    void Start()
    {
        timerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //update timeNow for protected access
        timeNow = System.DateTime.UtcNow.ToLocalTime();

        clockHour = timeNow.Hour;
        clockMinute = timeNow.Minute;
        clockSecond = timeNow.Second;

        //update clock from timeNow
        updateClock(clockHour, clockMinute, clockSecond);

        //update Timer
        updateTimer();
    }

    public void updateClock(int hour, int minute, int second){

        //pad 0s to 2 digit places
        clockText.text = hour.ToString().PadLeft(2,'0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2,'0');
    }

    public void updateTimer(){

        //Timer subtract while on
        if(timerActive){
            timer -= Time.deltaTime;
        }

        //Timer Deactivate when equals 0
        if (timer <= 0){
            timerActive = false;
            timer = 0;
        }

        //pad 0s to 2, and 3 digits
        timerText.text = ((int)timer/60).ToString().PadLeft(2,'0') + "m " //Convert Minutes
            + ((int)timer%60).ToString().PadLeft(2,'0') + "s " //Convert Seconds
            + ((int)(timer%1 * 1000)).ToString().PadLeft(3,'0') + "ms"; //Convert Milliseconds
    }

    public void TimerAdd(int minutes, int seconds, int milliseconds){

        //Add values to timer
        timer += minutes*60 + seconds + (milliseconds*.001f);

        //Check timer maximum time
        if(timer/60 > timerMaximumMinutes) {timer = timerMaximumMinutes*60;}

    }

    public void TimerToggle(){
        timerActive = !timerActive; //Switch active between true and false
    }
    public void TimerStart(){
        timerSaved = timer; //Save the current timer value for reset
        timerActive = true;
    }
    public void TimerReset(){
        timerActive = false; 
        timer = timerSaved; //Reload saved timer value
    }

    //Get Functions
    public int getTimerMinute(){
        return (int)(timer/60);
    }
    public int getTimerSecond(){
        return (int)(timer%60);
    }
    public int getTimerMillisecond(){
        return (int)(timer%1 * 1000);
    }

    //Set Function
    public void setTimer(int minutes, int seconds, int milliseconds){
        timer = minutes*60 + seconds + milliseconds*.001f;
    }

    //Button functions
    public void timerAddOneMin(){
        TimerAdd(1,0,0);
    }
    public void timerSubOneMin(){
        TimerAdd(-1,0,0);
    }
    public void timerAddOneSec(){
        TimerAdd(0,1,0);
    }
    public void timerSubOneSec(){
        TimerAdd(0,-1,0);
    }
    public void timerAdd100MS(){
        TimerAdd(0,0,100);
    }
    public void timerAddTenMS(){
        TimerAdd(0,0,10);
    }
    public void timerSubTenMS(){
        TimerAdd(0,0,-10);
    }
    public void timerSub100MS(){
        TimerAdd(0,0,-100);
    }
}
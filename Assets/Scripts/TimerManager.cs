using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class TimerManager : MonoBehaviour
{
    //Choose TextMeshPro for display
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI optionalDateText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stopwatchText;
    public int timerMaximumMinutes;
    public int stopwatchMaximumMinutes;

    //Variables for protected access
    protected System.DateTime timeNow;
    protected int clockHour;
    protected int clockMinute;
    protected int clockSecond;
    protected float timer;
    protected float timerSaved;
    protected float stopwatch;
    protected bool timerActive;
    protected bool stopwatchActive;

    // Start is called before the first frame update
    void Start()
    {
        timerActive = false;
        stopwatchActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //update clock from system Time
        updateClock();

        //update Timer
        updateTimer();

        //update Stopwatch
        updateStopwatch();
    }

    /////////////////////////////////////////////////// CLOCK

    //Clock Update
    public void updateClock(){

        //update timeNow for protected access
        timeNow = System.DateTime.UtcNow.ToLocalTime();
        clockHour = timeNow.Hour;
        clockMinute = timeNow.Minute;
        clockSecond = timeNow.Second;

        //Update Text - //pad 0s to 2 digit places
        clockText.text = clockHour.ToString().PadLeft(2,'0') + ":" + clockMinute.ToString().PadLeft(2,'0') + ":" + clockSecond.ToString().PadLeft(2,'0');
        
        if(optionalDateText != null){
            optionalDateText.text = timeNow.Month.ToString().PadLeft(2,'0') + "/" + timeNow.Day.ToString().PadLeft(2,'0') + "/" + timeNow.Year.ToString().PadLeft(4,'0'); //Optional Date
        }
    }
    public void updateClock(System.DateTime newTime){

        //update timeNow for protected access
        timeNow = newTime;
        clockHour = timeNow.Hour;
        clockMinute = timeNow.Minute;
        clockSecond = timeNow.Second;
        
        //Update Text - //pad 0s to 2 digit places
        clockText.text = clockHour.ToString().PadLeft(2,'0') + ":" + clockMinute.ToString().PadLeft(2, '0') + ":" + clockSecond.ToString().PadLeft(2,'0');

        if(optionalDateText != null){
            optionalDateText.text = timeNow.Month.ToString().PadLeft(2,'0') + "/" + timeNow.Day.ToString().PadLeft(2,'0') + "/" + timeNow.Year.ToString().PadLeft(4,'0'); //Optional Date
        }
    }

    //Clock Get Functions
    public System.DateTime getClockNow(){
        return timeNow;
    }
    public int getClockHours(){
        return timeNow.Hour;
    }
    public int getClockMinutes(){
        return timeNow.Minute;
    }
    public int getClockSeconds(){
        return timeNow.Second;
    }
    public int getDay(){
        return timeNow.Day;
    }
    public int getMonth(){
        return timeNow.Month;
    }
    public int getYear(){
        return timeNow.Year;
    }

    /////////////////////////////////////////////////// TIMER

    //Timer Main Functions
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

    //Timer Get Functions
    public int getTimerMinute(){
        return (int)(timer/60);
    }
    public int getTimerSecond(){
        return (int)(timer%60);
    }
    public int getTimerMillisecond(){
        return (int)(timer%1 * 1000);
    }

    //Timer Set Function
    public void setTimer(int minutes, int seconds, int milliseconds){
        timer = minutes*60 + seconds + milliseconds*.001f;
    }

    //Timer Button functions
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

    /////////////////////////////////////////////////// STOPWATCH

    //Stopwatch Update
    public void updateStopwatch(){
        
        //Stopwatch add while on
        if(stopwatchActive){
            stopwatch += Time.deltaTime;
        }

        //Timer Deactivate when equals 0
        if (stopwatch >= stopwatchMaximumMinutes*60){
            stopwatchActive = false;
            stopwatch = stopwatchMaximumMinutes*60;
        }

        stopwatchText.text = ((int)stopwatch/60).ToString().PadLeft(2,'0') + "m " //Convert Minutes
            + ((int)stopwatch%60).ToString().PadLeft(2,'0') + "s " //Convert Seconds
            + ((int)(stopwatch%1 * 1000)).ToString().PadLeft(3,'0') + "ms"; //Convert Milliseconds

    }
    
    //Stopwatch Main Functions
    public void stopwatchToggle(){
        stopwatchActive = !stopwatchActive;
    }
    public void stopwatchStart(){
        stopwatchActive = true;
    }
    public void stopwatchReset(){
        stopwatchActive = false;
        stopwatch = 0;
    }

    //Stopwatch Get Functions
    public int getStopwatchMinute(){
        return (int)(stopwatch/60);
    }
    public int getStopwatchSecond(){
        return (int)(stopwatch%60);
    }
    public int getStopwatchMillisecond(){
        return (int)(stopwatch%1 * 1000);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeState{
	NIGHT =0, //10PM - 6AM
	MORNING,  //6 AM - 11AM
	AFTERNOON,//11AM - 4PM
	EVENING   //4 PM - 10PM
};

public class GameClock : MonoBehaviour {

	//Default timescale is 1 min = hr. 

	public float timeScale = 60; //how many seconds to 1 hour

	public TimeState timeState;

	public int day = 0;
	public int hour = 0;
	private const int hoursInDay = 24;

	public float counter;
	float overflowCounter; //catch any overflow and add back to system.
	public float cumulativeOverflow; //just curious about how far we'd drift without the overflow counter
	//about 1% drift at 1 second intervals. Gets exponentially worse as intervals decrease. Not really an issue in practice, could cause problems with a time accelerator down the line though.
	//we should be good at anything up to 20x speed, assuming timescale = 60;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter >= timeScale) 
			AdvanceHour ();
		}

	void FixedUpdate(){

	}

	void AdvanceHour(){
		hour++;
		//catch the overflow and add it back to the counter for the next loop. add to cumulative so we can see how much it drifts.
		overflowCounter = counter - timeScale;
		cumulativeOverflow += overflowCounter;
		counter = overflowCounter;
		SetTimeState ();
		if (hour > hoursInDay)
			AdvanceDay ();
	}

	void AdvanceDay(){
		hour = 1;
		day++;
	}

	void SetTimeState(){
		if (hour >= 22 || hour < 6)
			timeState = TimeState.NIGHT;
		if (hour >= 6 &&  hour < 11)
			timeState = TimeState.MORNING;
		if (hour >= 11 && hour < 16)
			timeState = TimeState.AFTERNOON;
		if (hour >= 16 && hour < 22)
			timeState = TimeState.EVENING;
	}

}

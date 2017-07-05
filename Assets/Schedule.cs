using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Schedule : MonoBehaviour, ISchedule {

	//this is a sample schedule only.  To make a new one, simply add the ISchedule interface and add events to an array;

	public ScheduleEvent[] EventSchedule = new ScheduleEvent[GameClock.HoursInDay]; //set in inspector

	//the interface actually isnt needed anymore, simply drag and drop a bunch of ScheduleEvents onto this gameobject and they will be automatically loaded at game start, once this is loaded into the Scheduler, this gameobject will be destroyed remotely.


	//README
	//Proper structure is as so:
	//Main GameObject has Scheduler attached.  A child has this, Schedule, attached.  A child or children of this gameobject, has the ScheduleEvents attached.  This is so we can create prefabs of certain event chains.



	public ScheduleEvent[] ComposeSchedule () {
		
		foreach (ScheduleEvent e in gameObject.GetComponentsInChildren<ScheduleEvent>()) {
			EventSchedule [e.startHour] = e;
		}

		return EventSchedule;
	}

}

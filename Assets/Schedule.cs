using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Schedule : MonoBehaviour, ISchedule {

	[Tooltip("Simply a middleman that collects all the child ScheduleEvents and presents them to Scheduler. We can probably rework so this isnt needed later.")]
	public ScheduleEvent[] EventSchedule = new ScheduleEvent[GameClock.HoursInDay]; //set in inspector

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

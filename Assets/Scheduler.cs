using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scheduler : MonoBehaviour {

	//Check GameClock for whatever our timescale is.

	public Schedule schedule; //A schedule to load events from
	public GameClock clock;

	//for some reason the event isnt working, so heres a workarund for the time being.
	int lastHour;

	public ScheduleEvent[] EventSchedule;
	[SerializeField] private ScheduleEvent ActiveEvent; //just for visual display/debugging
	[SerializeField] private string ActiveEventName;

	// Use this for initialization
	void Start () {
		GrabReferences ();
		LoadSchedule ();
		clock.Register(this);
		clock.HourChangeEvent.AddListener (CheckSchedule); //does this actually work?
	}

	private void GrabReferences(){
		clock = GameObject.FindGameObjectWithTag ("Clock").GetComponent<GameClock>();
		schedule = gameObject.GetComponentInChildren<Schedule> ();
	}

	private void LoadSchedule(){
		EventSchedule = new ScheduleEvent[GameClock.HoursInDay];
		EventSchedule = schedule.ComposeSchedule ();
		//Destroy (schedule.gameObject);
	}

	public void CheckSchedule(){
	//	Debug.Log ("Checking schedule..");
		foreach (ScheduleEvent e in EventSchedule) {
			if (e != null) {
				if (e.startHour == clock.hour) {
					SetActive (e);
				}
				if (e.endHour == clock.hour) {
					SetInActive (e);
				}
			}
		}
	}

	void SetActive(ScheduleEvent e){
		ActiveEvent = e; //TODO
		ActiveEventName = e.name;
	//	Debug.Log (ActiveEvent.name);
	}

	void SetInActive(ScheduleEvent e){
		if (ActiveEvent == e) {
			ActiveEvent = null;
			ActiveEventName = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (lastHour != clock.hour)
			CheckSchedule ();
		lastHour = clock.hour;
	}
}

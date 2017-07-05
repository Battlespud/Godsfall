using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scheduler : MonoBehaviour {

	//Check GameClock for whatever our timescale is.
	[Tooltip("This component should be placed on the character game object along with movement controller.  A Schedule component must be placed on a child object (ScheduleBlock) then ScheduleEvents should be added to a child of that gameobject. \n They will be dynamically loaded at the start of the" +
		" game, after which point they remain as references. ")]
	public bool HoverForInstructions = false;
	public Schedule schedule; //A schedule to load events from
	public GameClock clock;
	public MovementController movementController;

	bool SingleEvent = false;

	//for some reason the event isnt working, so heres a workarund for the time being.
	int lastHour;

	[Tooltip("This is a collection of events, their index is equal to their starting hour. Empty values are ok. \n If an event is present in slot 0, no other events will be used.")]
	public ScheduleEvent[] EventSchedule;
	private ScheduleEvent ActiveEvent; 
	[SerializeField] private string ActiveEventName;

	// Use this for initialization
	void Start () {
		GrabReferences ();
		LoadSchedule ();
		clock.Register(this);
		clock.HourChangeEvent.AddListener (CheckSchedule); //does this actually work?
	}

	private void GrabReferences(){
		movementController = gameObject.GetComponent<MovementController> ();
		clock = GameObject.FindGameObjectWithTag ("Clock").GetComponent<GameClock>();
		schedule = gameObject.GetComponentInChildren<Schedule> ();
	}

	private void LoadSchedule(){
		EventSchedule = new ScheduleEvent[GameClock.HoursInDay];
		EventSchedule = schedule.ComposeSchedule ();
		if (EventSchedule [0] != null) {
			SingleEvent = true;
			ActiveEvent = EventSchedule [0];
		}
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
		if (lastHour != clock.hour && !SingleEvent)
			CheckSchedule ();
		lastHour = clock.hour;
		DoActiveEvent ();
	}

	void DoActiveEvent (){
		if (ActiveEvent != null && ActiveEvent.isFinished() != true) {
			movementController.agentInputToMove (ActiveEvent.GetDestination());
			if (Vector3.Distance (transform.position, ActiveEvent.GetDestination ()) < .25f) {
				ActiveEvent.ReachTarget ();
			}
		}
	}
}

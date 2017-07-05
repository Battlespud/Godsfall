using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleEvent : MonoBehaviour{

	public string name;
	public int startHour;
	public int endHour;

	public ScheduleEvent(string s, int i, int e){
		name = s;
		startHour = i;
		endHour = e;
	}
	public ScheduleEvent(string s, TimeState t){
		name = s;
		Vector2 vec = GameClock.GetHours (t);
		startHour = (int)vec.x;
		endHour = (int)vec.y;
	}

	public ScheduleEvent Clone(ScheduleEvent s){
		return new ScheduleEvent (s.name, startHour, endHour);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleEvent : MonoBehaviour{

	public string name;
	[Tooltip("Setting this to 0 will make the event run constantly all day, as 0 is not a naturally reachable point, looping is recommended.")]
	public int startHour;
	public int endHour;
	[Tooltip("Place meshless gameobjects where you want to go, then just drag and drop here, they must intersect a navmesh.")]
	public List<GameObject> targetObjects; //used to mark where to go
	int Index=0;
	public bool Loop = false;
	bool finished = false;
	public bool isFinished(){
		return finished;
	}


	public void ReachTarget(){
		Index++;
		if (Index >= targetObjects.Count) {
			if (Loop) {
				Index = 0;
			} else {
				finished = true;
			}
		}
	}

	void Start(){
		if (targetObjects.Count < 1)
			finished = true;
	}

	public Vector3 GetDestination(){
		return targetObjects [Index].transform.position;
	}

	//These constructors shouldnt really ever be used except for bug checking
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

}

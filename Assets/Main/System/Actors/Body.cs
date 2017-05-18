using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body  {

	//pointer to parent
	public Entity parentEntity;
	public List<BodyPart> bodyPartsList = new List<BodyPart>();

	void setupHumanBody(){
		bodyPartsList.Add(new BodyPart(0,0));
		bodyPartsList.Add(new BodyPart(0,1));
		bodyPartsList.Add(new BodyPart(1,0));
		bodyPartsList.Add(new BodyPart(1,1));
		bodyPartsList.Add(new BodyPart(2,0));
		bodyPartsList.Add(new BodyPart(2,1));
	}

	public Body(){
		setupHumanBody ();
		foreach(BodyPart b in bodyPartsList){
			b.parentBody = this;
		}
		Debug.Log ("Body setup");
	}
}

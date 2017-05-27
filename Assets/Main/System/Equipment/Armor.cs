using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment {

	//material followed by body part
	string[,] armorStringArray = new string[7,4]{{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},{"Helm","Gauntlet","Greave","Cuirass"},};

	//where this gets equipped
	public BodyPartType bodyPartType;


	public Armor(Materials mat, BodyPartType partType){
		material = mat;
		bodyPartType = partType;
		name =string.Format("{0} {1}", material, armorStringArray[material,bodyPartType]);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment {

	//material followed by body part                                                                                                                         
	string[,] armorStringArray = new string[7,4]
    {
        {"Helmet of Safety","Mittens of bloody knuckles","Pants of the splintered scrotum","Shirt of chaffed nipples"}, // wood
        {"Helm","Gloves","Leggings","Breastplate"}, // copper
        {"Helm","Gauntlet","Leggings","Breastplate"}, // bronze
        {"Helm","Gauntlet","Greave","Cuirass"}, // iron
        {"Helm","Gages","Leggings","Segmentata"}, // ironwood
        {"Face Plate","Gauntlets","Greaves","Cuirass"}, // silver
        {"Head Piece","Gages","Greaves","Segmentata"} // relic
    };

	//where this gets equipped
	public BodyPartType bodyPartType;


	public Armor(Materials mat, BodyPartType partType){
		material = mat;
		bodyPartType = partType;
		name = string.Format("{0} {1}", material, armorStringArray[(int)material,(int)bodyPartType]);
	}


}

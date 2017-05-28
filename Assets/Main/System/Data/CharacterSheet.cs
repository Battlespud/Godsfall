using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet {


	public AttributeSheet attributeSheet;
	public SkillSheet skillSheet;

	public int[] nullArray;

	public CharacterSheet(){
		nullArray = new int[7]{ 0, 0, 0, 0, 0, 0, 0 }; //just for testing
		attributeSheet = new AttributeSheet(nullArray);
		skillSheet = new SkillSheet (nullArray);
	}
}

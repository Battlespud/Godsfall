using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSheet {

	public enum Attribute{
		STR=1,
		CON=2,
		DEX=3,
		INT=4,
		WIS=5,
		CHA=6
	};

	//arrays are zero indexed, just ignore 0 place for now
	public int[] AttributeArray;

	public AttributeSheet(int[] array){
		AttributeArray = array;
	}

}

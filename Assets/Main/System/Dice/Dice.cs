using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice  {


	public static int roll(int sides = 6){
		return (Random.Range (1, sides + 1));
	}

	public static int d6(){
		return (Random.Range (1, 7));
	}

	public static int d20(){
		return (Random.Range (1, 21));
	}

	public static int multiRoll(int dice, int sides){
		return (Random.Range (1 * dice, sides * 3 + 1));
	}


}

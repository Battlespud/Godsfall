using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
	void takeDamage(int hpLost);
	void heal (int hpGain);

	void destroyed();
}

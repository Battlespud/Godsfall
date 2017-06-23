using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible {

	void IDestroy();
	void ITakeDamage(int i);
	void IInitializeHPEvents();
}

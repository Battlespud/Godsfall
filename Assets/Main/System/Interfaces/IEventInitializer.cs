using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventInitializer {

	void initializeEvents();

	//this method must be called from the parent class AFTER construction and setting of parentbody or equivalent variable, or a
	//unexpected range exception will be generated
}

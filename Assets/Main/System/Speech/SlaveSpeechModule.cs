using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveSpeechModule : MonoBehaviour {

	//place on an existing speechblock then slave to a master. Will coopt speechblocks canvas to announce messages.

	private SpeechModule slaveModule;
	public SpeechMaster master;


	void Start () {
		slaveModule = gameObject.GetComponent<SpeechModule> ();
		Enslave ();
		Register ();
	}

	void Register(){
		master.register (this);
	}
	void Enslave(){
		slaveModule.SlaveMode = true;
	}


	public void Speak(SpeechKey key){
		slaveModule.Speak (key);
	}

	public void Unspeak(){
		slaveModule.Unspeak ();
	}





	void Update () {
		
	}





}

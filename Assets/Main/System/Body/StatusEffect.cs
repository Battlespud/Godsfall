using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour , IEventInitializer {

	//a status effect is added to the list in Body and will cause an effect for a given duration before selfdestructing.
	//they are not designed to be edited individually beyond being removed or added.

	const int TICK = 1;

	public StatusEffectEvent destroyStatusEffect;
	public StatusEffectEvent initializeStatusEffect;

	private bool modifiable = true;
	public bool initialized = false;

	public string effectName;
	BodyPart affectedBodyPart;
	Entity affectedActor;
	Body affectedBody;
	int hpDamagePerSecond;
	public float duration; //in seconds
	public float durationTimer = 0f;
	int bloodLossPerSecond;
	bool stunEffect;
	bool slowEffect;
	float slowSeverity;

	public void initalizeEffect(Entity e, BodyPart b = null, int hpDam = 0, int bloodDam = 0, 
		bool stunE = false, bool slowE = false, float slowSev = 0, float dur = 10, string nam = "default effect name"){
		if (!modifiable) {
			return;
		}
		modifiable = false;

		affectedActor = e;
		affectedBodyPart = b;
		affectedBody = e.body;
		hpDamagePerSecond = hpDam;
		bloodLossPerSecond = bloodDam;
		stunEffect = stunE;
		slowEffect = slowE;
		slowSeverity = slowSev;
		effectName = nam;

		duration = dur;
		initialized = true;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dealDamage ();
		checkTimer ();

		durationTimer += TICK * Time.deltaTime;
	}

	void dealDamage(){
		affectedActor.modifyBlood ((int)(-bloodLossPerSecond*Time.deltaTime));
		if (affectedBodyPart != null) {
			affectedBodyPart.hitPoints.modifyHP (-hpDamagePerSecond);
		}
	}

	void checkTimer(){
		if (durationTimer >= duration)
				destroyStatusEffect.Invoke (this);
	}


	//implement events
	public void initializeEvents(){
		destroyStatusEffect = new StatusEffectEvent ();
		destroyStatusEffect.AddListener (affectedBody.removeStatusEffect);

	}

}

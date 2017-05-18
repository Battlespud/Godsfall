using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenBoneEventArgs : UnityEvent<Entity, Bone> {

	public Entity entity;
	public Bone bone;

	public BrokenBoneEventArgs(Entity e, Bone b){
		entity = e;
		bone = b;
	}

}

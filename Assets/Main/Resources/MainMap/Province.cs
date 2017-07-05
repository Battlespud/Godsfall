using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour, IFaction {

	//Materials
	public  Material[] FactionMaterials = new Material[Faction.MaxFactions];
	 bool initialized = false;



	Faction faction;

	//Visuals
	private Renderer CityRenderer;
	public Material CityMaterial;
	public Material Test;
		//linerenderer

	//Functional
	[SerializeField]public List<Province> Connected;
	public string name;

	void Start () {
		//Connected = new List<Province> ();
		name = gameObject.name;
		Test = Resources.Load ("MainMap/Faction0") as Material;
		faction = gameObject.GetComponent<Faction> ();
		faction.dependents.Add (this);
		CityRenderer = gameObject.GetComponent<Renderer> ();
		if (!initialized) {
			OneTimeSetup ();
			initialized = true;
		}
		ProvinceManager.RegisterProvince (this, Connected);
		ChangeFaction (faction.FactionID);	
	}

	//loads all the materials into the statics
	private void OneTimeSetup()
	{
		string path = "MainMap/Faction";
		for (int i = 0; i < Faction.MaxFactions; i++) {
			FactionMaterials[i] = Resources.Load(string.Format("{0}{1}",path,i)) as Material;
		}
		//Debug.Log ("Loaded resources");
	}



	void Update () {
		
	}



	public void ChangeFaction(int i)
	{
		faction.ChangeFaction (i);
		CityRenderer.material = FactionMaterials[i];
	}

	public Faction GetFaction(){
		return faction;
	}

	private void ChooseMaterial(int i){
		CityMaterial = FactionMaterials [i];
	}

}

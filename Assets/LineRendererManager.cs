using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour {

	List<GameObject> LineRendererObjects;


	void Start(){
		LineRendererObjects = new List<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
		if (ProvinceManager.redraw ||  FactionMatrix.hasChanged) {
			Debug.Log ("Redrawing");
			ReDraw ();
			ProvinceManager.redraw = false;
			FactionMatrix.hasChanged = false;
		}
	}

	void ReDraw(){

		for (int i = 0; i < LineRendererObjects.Count; i++) {
			Destroy (LineRendererObjects[i]);
		}
		Color c;
		List<ProvinceManager.ProvinceConnection> ProvinceConnections = ProvinceManager.GetConnectionsList ();
				foreach (ProvinceManager.ProvinceConnection con in ProvinceConnections) {
			
			GameObject slaveRenderer = new GameObject();
			slaveRenderer.transform.SetParent(gameObject.transform);
			slaveRenderer.name = string.Format("LineRenderer: {0} - {1}",con.a.name,con.b.name);

			LineRenderer t = slaveRenderer.AddComponent <LineRenderer>();
			t.SetPositions(new Vector3[2]{con.a.transform.position, con.b.transform.position});
			t.SetWidth (.05f, .05f);
			t.material.shader = Shader.Find ("Unlit/Color");
		//	Debug.Log("Switching over faction diplomacy");
			Diplo d = FactionMatrix.GetRelationship (con.a.GetFaction ().FactionID, con.b.GetFaction ().FactionID);
		//	Debug.Log (d);
			switch(d)
				{
			case(Diplo.WAR):
				{
					c = Color.red;
					break;
				}
			case(Diplo.PEACE):
				{
					c = Color.yellow;
					break;
				}
			case(Diplo.FRIEND):
				{
					c = Color.blue;
					break;
				}
			case(Diplo.ALLY):
				{
					c = Color.green;
					break;
				}
			default:
				{
					c=Color.gray;
					break;
				}

				}//end switch
			t.material.color = c;
			t.material.color = c;
			}
	}
}

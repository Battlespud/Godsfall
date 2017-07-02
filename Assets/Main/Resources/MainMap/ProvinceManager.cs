using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProvinceManager {

	static List<ProvinceConnection> ProvinceConnections = new List<ProvinceConnection> ();

	public static bool redraw = false;


	public struct ProvinceConnection{
		public	Province a, b;

		public ProvinceConnection(Province i, Province e){
			a = i;
			b = e;
		}
	}


	public static void RegisterProvince(Province source, List<Province> Connections){
		foreach (Province targ in Connections) {
			ProvinceConnections.Add(new ProvinceConnection(source,targ));
		}
		ReDraw ();
	}

	public static List<ProvinceConnection> GetConnectionsList(){
		return ProvinceConnections;
	}


	static void ReDraw(){
		redraw = true;
	}

}

using UnityEngine;
using System.Collections;

public class ShipExplosion : MonoBehaviour {
	
	ParticleSystem ps;
	
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	void Update () {
		if (!ps.IsAlive()) {
			Destroy(gameObject);
		}
	}
}

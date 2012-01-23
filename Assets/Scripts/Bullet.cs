using UnityEngine;
using System.Collections;

public class Bullet {	
	private Transform transform;
	private Vector3 velocity = Vector3.zero;
	
	public Bullet( Transform t ) {
		transform = t;
	}
	
	public void Tick() {
		transform.position += velocity * Time.deltaTime;
	}
	
	public void SetVelocity( Vector3 velo ) {
		velocity = velo;
	}
}

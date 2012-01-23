using UnityEngine;
using System.Collections;

public class Bullet {	
	private Transform transform;
	private Vector3 velocity = Vector3.zero;
	
	public Bullet( Transform t ) {
		transform = t;
	}
	
	public void SetInactive() {
		transform.gameObject.active = false;
		velocity = Vector3.zero;
		transform.position = Vector3.one * 999f;
	}
	
	public void SetActive() {
		transform.gameObject.active = true;
	}
	
	public void Tick() {
		transform.position += velocity * Time.deltaTime;
	}
	
	public void SetPosition( Vector3 pos ) {
		transform.position = pos;
	}
	
	public void AddVelocity( Vector3 velo ) {
		velocity += velo;
	}
	
	public void SetVelocity( Vector3 velo ) {
		velocity = velo;
	}
}

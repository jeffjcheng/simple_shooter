using UnityEngine;
using System.Collections;

public class Bullet {
	private BulletManager manager;
	private Transform transform;
	private Vector3 velocity = Vector3.zero;
	
	public Vector3 position {
		get { return transform.position; }
	}
	
	public Bullet( Transform t, BulletManager manager ) {
		transform = t;
		this.manager = manager;
	}
	
	public void SetInactive() {
		manager.AddInactiveBullet( this );
		transform.gameObject.active = false;
		velocity = Vector3.zero;
		transform.position = Vector3.one * 999f;
	}
	
	public void SetActive() {
		transform.gameObject.active = true;
	}
	
	public void Tick() {
		if( transform.position.x > 5.25f || transform.position.x <= -5.25f ) {
			SetInactive();
		} else if( transform.position.y > 7.75f || transform.position.y < -7.75f ) {
			SetInactive();
		}
		
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

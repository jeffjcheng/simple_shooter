using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private float speed = 5f;
	
	private Vector2 touchStart = Vector2.zero;
	
	private BulletManager manager;
	
	void Start() {
		manager = GetComponent<BulletManager>();
		
		manager.DoCoroutine( PlayerShots );
	}
	
	void Update() {
		if( Input.touches.Length > 0 ) {
			ProcessTouch( Input.GetTouch( 0 ) );
		}
	}
	
	void ProcessTouch( Touch t ) {
		switch( t.phase ) {
		case TouchPhase.Began:
			touchStart = t.position;
			break;
			
		case TouchPhase.Moved:
			// [ -4.75, 4.75 ]
			// [ -7.25, 6.75 ]
			Vector2 delta = t.position - touchStart;
			transform.position += (Vector3)delta.normalized * speed * Time.deltaTime;
			break;
			
		case TouchPhase.Canceled:
		case TouchPhase.Ended:
			break;
			
		case TouchPhase.Stationary:
			break;
		}
	}
	
	IEnumerator PlayerShots( BulletManager mgr, params object[] o ) {
		while( true ) {
			yield return new WaitForSeconds( 0.125f );
			Bullet b = mgr.GetBullet();
			b.SetPosition( transform.position );
			b.SetVelocity( Vector3.up * 15f );
		}
	}
}

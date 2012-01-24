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
		case TouchPhase.Stationary:
			Vector2 delta = t.position - touchStart;
			Vector3 v3 = (Vector3)delta.normalized * speed * Time.deltaTime;
			
			if( transform.position.x < -4.75f ) {
				v3.x = Mathf.Max( v3.x, 0f );
			} else if( transform.position.x > 4.75f ) {
				v3.x = Mathf.Min( v3.x, 0f );
			}
			
			if( transform.position.y < -7.25f ) {
				v3.y = Mathf.Max( v3.y, 0f );
			} else if( transform.position.y > 6.75f ) {
				v3.y = Mathf.Min( v3.y, 0f );
			}
			
			transform.position += v3;
			break;
			
		case TouchPhase.Canceled:
		case TouchPhase.Ended:
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

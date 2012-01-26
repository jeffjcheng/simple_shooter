using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BulletPatterns {
	public static IEnumerator ToPlayer( BulletManager mgr, Player p ) {
		float rof = 1.25f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			Vector3 dir = p.position - mgr.position;
			
			Bullet b = mgr.GetBullet();
			b.SetPosition( mgr.position );
			b.SetVelocity( dir.normalized * 5f );
		}
	}
	
	public static IEnumerator Radial( BulletManager manager, Player plr ) {
		float rof = 3f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			
			float time = 0f;
			while( time < 1f ) {
				float angle = -Mathf.PI*2f*(time-0.25f) + Random.value*Mathf.PI/8f;
				
				Bullet b = manager.GetBullet();
				b.SetVelocity( new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * 2.5f );
				b.SetPosition( manager.position );
				
				manager.StartCoroutine( Stop( b ) );
				manager.StartCoroutine( SeekPlayer( b, plr ) );
				
				yield return new WaitForFixedUpdate();
				time += Time.fixedDeltaTime;
			}
		}
	}
	
	static IEnumerator Stop( Bullet b ) { // radial helper
		yield return new WaitForSeconds( 1f );
		b.SetVelocity( Vector3.zero );
	}
	
	static IEnumerator SeekPlayer( Bullet b, Player p ) { // radial helper
		yield return new WaitForSeconds( 2.5f );
		b.SetVelocity( (p.position - b.position).normalized * 15f );
	}
	
	public static IEnumerator Circle( BulletManager manager ) {
		float rof = 3.5f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			
			float progress = 0f;
			while( progress < 1f ) {
				float angle = Mathf.PI*2f*(progress+0.046875f);
				
				Bullet b = manager.GetBullet();
				b.SetVelocity( new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * 7.5f );
				b.SetPosition( manager.position );
				
				progress += 0.03125f;
			}
		}
	}
	
	public static IEnumerator Ripple( BulletManager mgr ) {
		float rof = 0.75f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			
			float diff = 0f;
			while( diff <= 0.5f ) {
				float angle = -Mathf.PI*2f*(diff);
				
				Bullet b = mgr.GetBullet();
				b.SetVelocity( new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * 5f );
				b.SetPosition( mgr.position );
				
				diff += 0.0625f;
			}
			
			yield return new WaitForSeconds( rof );
			diff = 0f;
			while( diff <= 0.5f ) {
				float angle = -Mathf.PI*2f*(diff+0.03125f);
				
				Bullet b = mgr.GetBullet();
				b.SetVelocity( new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * 5f );
				b.SetPosition( mgr.position );
				
				diff += 0.0625f;
			}
		}
	}
	
	public static IEnumerator Burst( BulletManager mgr, Player plr ) {
		float rof = 1.75f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			
			for( int a = 0 ; a < 5 ; a++ ) {
				Vector3 dir = plr.position - mgr.position;
				dir.Normalize();
				
				Bullet b = mgr.GetBullet();
				b.SetPosition( mgr.position );
				b.SetVelocity( dir * 10f );
				
				float angle = Mathf.Atan2( dir.y, dir.x );
				
				b = mgr.GetBullet();
				b.SetPosition( mgr.position );
				b.SetVelocity( new Vector3( Mathf.Cos( angle+Mathf.PI/16f ), Mathf.Sin( angle+Mathf.PI/16f ) ) * 10f );
				
				b = mgr.GetBullet();
				b.SetPosition( mgr.position );
				b.SetVelocity( new Vector3( Mathf.Cos( angle-Mathf.PI/16f ), Mathf.Sin( angle-Mathf.PI/16f ) ) * 10f );
				
				yield return new WaitForSeconds( 0.125f );
			}
		}
	}
}

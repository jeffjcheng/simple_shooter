using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BulletPatterns {
	public static IEnumerator ToPlayer( BulletManager mgr, Player p ) {
		float rof = 3f;
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
			
			List<Bullet> bullets = new List<Bullet>();
			
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
	
	public static IEnumerator Stop( Bullet b ) {
		yield return new WaitForSeconds( 1f );
		b.SetVelocity( Vector3.zero );
	}
	
	public static IEnumerator SeekPlayer( Bullet b, Player p ) {
		yield return new WaitForSeconds( 3f );
		b.SetVelocity( (p.position - b.position).normalized * 7.5f );
	}
}

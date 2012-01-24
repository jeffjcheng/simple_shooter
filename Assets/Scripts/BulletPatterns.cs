using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BulletPatterns {
	public static IEnumerator Radial( BulletManager manager, params object[] o ) {
		float rof = 3f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			
			List<Bullet> bullets = new List<Bullet>();
			
			float time = 0f;
			while( time < 1f ) {
				float angle = -Mathf.PI*2f*time + Random.value*Mathf.PI/8f;
				
				Bullet b = manager.GetBullet();
				b.SetVelocity( new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * 10f );
				b.SetPosition( Vector3.zero );
				
				
				yield return new WaitForFixedUpdate();
				time += Time.fixedDeltaTime;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine {
	public delegate IEnumerator DelayedAction( params object[] o );
}

public class BulletManager : MonoBehaviour {
	private static BulletManager ins;
	public GameObject bulletPrefab;
	private List<Bullet> bullets;
	private Queue<Bullet> inactive;
	private Transform myBullets;

	void Start () {
		myBullets = new GameObject( gameObject.name+"Bullets" ).transform;
		bullets	 = new List<Bullet>( 10 );
		inactive = new Queue<Bullet>( 25 );
		
		ins = this;
		
		// start with some bullets
		for( int a = 0 ; a < 25 ; a++ ) {
			GameObject go = (GameObject)Instantiate( bulletPrefab );
			go.transform.parent = myBullets;
			Bullet b = new Bullet( go.transform );
			b.SetInactive();
			
			inactive.Enqueue( b );
		}
		
		StartCoroutine( first( ) );
	}
	
	void Update () {
		foreach( Bullet b in bullets ) {
			b.Tick();
		}
	}
	
	public static void DoCoroutine( DelayedAction func, params object[] o ) {
		ins.StartCoroutine( func( o ) );
	}
	
	public static Bullet GetBullet() {
		if( ins.inactive.Count > 0 ) {
			Bullet bullet = ins.inactive.Dequeue();
			bullet.SetActive();
			ins.bullets.Add( bullet );
			return bullet;
		}
		
		GameObject go = (GameObject)Instantiate( ins.bulletPrefab );
		go.transform.parent = ins.myBullets;
		Bullet b = new Bullet( go.transform );
		ins.bullets.Add( b );
		return b;
	}
	
	IEnumerator first() {
		float rof = 0.5f;
		while( true ) {
			yield return new WaitForSeconds( rof );
			Bullet b = GetBullet();
			b.SetPosition( transform.position );
			b.SetVelocity( Vector3.down );
			DoCoroutine( test, b );
		}
	}
	
	IEnumerator test( params object[] o ) {
		yield return new WaitForSeconds( Random.value+0.5f );
		((Bullet)o[0]).SetVelocity( new Vector3( 0.5f - Random.value, 0f, 0f ) );
	}
}

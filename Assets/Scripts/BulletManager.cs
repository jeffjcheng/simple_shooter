using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine {
	public delegate IEnumerator DelayedAction( BulletManager manager, params object[] o );
}

public class BulletManager : MonoBehaviour {
	public GameObject bulletPrefab;
	private List<Bullet> bullets;
	private Queue<Bullet> inactive;
	private Transform myBullets;
	private int aaa;

	void Start () {
		myBullets = new GameObject( gameObject.name+"Bullets" ).transform;
		bullets	 = new List<Bullet>();
		inactive = new Queue<Bullet>();
		
		// start with some bullets
//		for( int a = 0 ; a < 10 ; a++ ) {
//			GameObject go = (GameObject)Instantiate( bulletPrefab );
//			go.transform.parent = myBullets;
//			Bullet b = new Bullet( go.transform, this );
//			b.SetInactive();
//			
//			inactive.Enqueue( b );
//		}
	}
	
	void Update () {
		aaa = inactive.Count;
		Bullet[] b = bullets.ToArray();
		for( int a = 0 ; a < b.Length ; a++ ) {
			b[a].Tick();
		}
	}
	
	public void DoCoroutine( DelayedAction func, params object[] o ) {
		StartCoroutine( func( this, o ) );
	}
	
	public Bullet GetBullet() {
		if( inactive.Count > 0 ) {
			Bullet bullet = inactive.Dequeue();
			bullet.SetActive();
			bullets.Add( bullet );
			return bullet;
		}
		
		GameObject go = (GameObject)Instantiate( bulletPrefab );
		go.transform.parent = myBullets;
		Bullet b = new Bullet( go.transform, this );
		bullets.Add( b );
		return b;
	}
	
	public void AddInactiveBullet( Bullet b ) {
		if( bullets.Contains( b ) ) {
			bullets.Remove( b );
		}
		inactive.Enqueue( b );
	}
}

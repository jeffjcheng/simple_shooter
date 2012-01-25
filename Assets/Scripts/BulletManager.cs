using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	public GameObject bulletPrefab;
	private List<Bullet> bullets;
	private Queue<Bullet> inactive;
	private Transform myBullets;
	private int aaa;
	
	public Vector3 position {
		get { return transform.position; }
	}

	void Start () {
		myBullets = new GameObject( gameObject.name+"Bullets" ).transform;
		bullets	 = new List<Bullet>();
		inactive = new Queue<Bullet>();
	}
	
	void Update () {
		aaa = inactive.Count;
		Bullet[] b = bullets.ToArray();
		for( int a = 0 ; a < b.Length ; a++ ) {
			b[a].Tick();
		}
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

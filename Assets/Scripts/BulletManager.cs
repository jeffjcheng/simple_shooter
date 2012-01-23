using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	public GameObject bulletPrefab;
	private List<Transform> bullets;
	private List<Transform> inactive;

	// Use this for initialization
	void Start () {
		bullets = new List<Transform>( 50);
		inactive = new List<Transform>( 50 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

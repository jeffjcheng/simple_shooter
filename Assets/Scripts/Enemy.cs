using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Player plr;
	private BulletManager manager;
	
	void Start() {
		plr = GameObject.Find( "Player" ).GetComponent<Player>();
		manager = GetComponent<BulletManager>();
		
		manager.StartCoroutine( BulletPatterns.ToPlayer( manager, plr ) );
		manager.StartCoroutine( BulletPatterns.Radial( manager, plr ) );
	}
	
	void Update() {
		
	}
}

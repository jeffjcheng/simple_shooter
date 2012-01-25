using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Player plr;
	private BulletManager manager;
	
	void Start() {
		plr = GameObject.Find( "Player" ).GetComponent<Player>();
		manager = GetComponent<BulletManager>();
		
		StartCoroutine( Attack() );
	}
	
	
	IEnumerator Attack() {
		manager.StartCoroutine( BulletPatterns.ToPlayer( manager, plr ) );
		yield return new WaitForSeconds( 2f );
		manager.StartCoroutine( BulletPatterns.Circle( manager ) );
		yield return new WaitForSeconds( 2f );
		manager.StartCoroutine( BulletPatterns.Ripple( manager ) );
		yield return new WaitForSeconds( 2f );
		manager.StartCoroutine( BulletPatterns.Radial( manager, plr ) );
	}
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	void Start() {
		GameObject.Find( "HighScore" ).guiText.text = "High Score : "+PlayerPrefs.GetInt( "highscore", 0 );
		StartCoroutine( WaitForInput() );
		Player.OnPlayerDeath += OnPlayerDeath;
	}
	
	IEnumerator WaitForInput() {
		bool receivedInput = false;
		while( !receivedInput ) {
			yield return null;
			
			if( Input.touches.Length > 0 || Input.GetKeyDown( KeyCode.Z ) ) {
				receivedInput = true;
			}
		}
		
		GameObject.Find( "Player" ).GetComponent<Player>().enabled = true;
		GameObject.Find( "Enemy" ).GetComponent<Enemy>().enabled = true;
		Destroy( GameObject.Find( "Start" ) );
	}
	
	void OnPlayerDeath() {
		StartCoroutine( WaitForRespawnInput() );
	}
	
	IEnumerator WaitForRespawnInput() {
		yield return new WaitForSeconds( 1.5f );
		GameObject.Find( "Respawn" ).guiText.enabled = true;
		bool receivedInput = false;
		while( !receivedInput ) {
			yield return null;
			
			if( Input.touches.Length > 0 || Input.GetKeyDown( KeyCode.Z ) ) {
				Application.LoadLevel( Application.loadedLevel );
			}
		}
	}
}

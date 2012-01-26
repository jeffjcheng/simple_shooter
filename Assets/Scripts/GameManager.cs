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
		GameObject.Find( "Respawn" ).guiText.enabled = true;
		StartCoroutine( WaitForRespawnInput() );
	}
	
	IEnumerator WaitForRespawnInput() {
		bool receivedInput = false;
		while( !receivedInput ) {
			yield return null;
			
			if( Input.touches.Length > 0 || Input.GetKeyDown( KeyCode.Z ) ) {
				Application.LoadLevel( Application.loadedLevel );
			}
		}
	}
}

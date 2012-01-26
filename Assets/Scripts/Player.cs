using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public delegate void PlayerDeathEvent();
	public static event PlayerDeathEvent OnPlayerDeath;
	public Transform joystick;
	public GUIText scoreGUI;
	
	private float speed = 5f;
	private Vector2 touchStart = Vector2.zero;
	
	private BulletManager manager;
	
	private int score = 0;
	
	public Vector3 position {
		get { return transform.position; }
	}
	
	void Start() {
		manager = GetComponent<BulletManager>();
		
		manager.StartCoroutine( PlayerShots() );
		StartCoroutine( ScorePulse() );
	}
	
	void Update() {
		if( Input.touches.Length > 0 ) {
			ProcessTouch( Input.GetTouch( 0 ) );
		}
	}
	
	IEnumerator ScorePulse() {
		while( true ) {
			yield return new WaitForSeconds( 1f );
			score += 10;
			scoreGUI.text = "Score : "+score;
		}
	}
	
	void ProcessTouch( Touch t ) {
		switch( t.phase ) {
		case TouchPhase.Began:
			touchStart = t.position;
			joystick.position = GetWorldPosFromTouch( touchStart );
			joystick.renderer.enabled = true;
			break;
			
		case TouchPhase.Moved:
		case TouchPhase.Stationary:
			Vector2 delta = t.position - touchStart;
			Vector3 v3 = (Vector3)delta.normalized * speed * Time.deltaTime;
			joystick.localEulerAngles = new Vector3( 0f, 0f, Mathf.Atan2( delta.y, delta.x ) * 180f / Mathf.PI - 90f );
			
			if( transform.position.x < -4.75f ) {
				v3.x = Mathf.Max( v3.x, 0f );
			} else if( transform.position.x > 4.75f ) {
				v3.x = Mathf.Min( v3.x, 0f );
			}
			
			if( transform.position.y < -7.25f ) {
				v3.y = Mathf.Max( v3.y, 0f );
			} else if( transform.position.y > 6.75f ) {
				v3.y = Mathf.Min( v3.y, 0f );
			}
			
			transform.position += v3;
			break;
			
		case TouchPhase.Canceled:
		case TouchPhase.Ended:
			joystick.renderer.enabled = false;
			break;
		}
	}
	
	IEnumerator PlayerShots() {
		while( true ) {
			yield return new WaitForSeconds( 0.125f );
			Bullet b = manager.GetBullet();
			b.SetPosition( transform.position );
			b.SetVelocity( Vector3.up * 15f );
		}
	}
	
	Vector3 GetWorldPosFromTouch( Vector2 v2 ) {
		Vector3 pos = (Vector3)v2;
		pos.z = 5f;
		
		pos.x /= Screen.width;
		pos.x *= 10f;
		pos.x -= 5f;
		
		pos.y /= Screen.height;
		pos.y *= 15f;
		pos.y -= 7.5f;
		
		return pos;
	}
	
	void OnCollisionEnter( Collision c ) {
		this.StopAllCoroutines();
		ParticleSystem explosion = GameObject.Find( "Explosion" ).GetComponent<ParticleSystem>();
		explosion.transform.position = transform.position;
		explosion.Play();
		transform.position = new Vector3( 0f, -100f, 0f );
		this.enabled = false;
		
		if( score > PlayerPrefs.GetInt( "highscore", 0 ) ) {
			PlayerPrefs.SetInt( "highscore", score );
		}
		
		GameObject.Find( "HighScore" ).guiText.text = "High Score : "+PlayerPrefs.GetInt( "highscore" );
		
		if( OnPlayerDeath != null ) OnPlayerDeath();
		OnPlayerDeath = null;
	}
}

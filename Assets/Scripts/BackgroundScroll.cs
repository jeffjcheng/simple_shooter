using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Renderer ) )]
public class BackgroundScroll : MonoBehaviour {
	public float offsetDelta = 0.01f;
	private Material m;
	
	void Start() {
		m = renderer.material;
	}

	void Update() {
		m.mainTextureOffset += new Vector2( 0f, offsetDelta * Time.deltaTime );
	}
}

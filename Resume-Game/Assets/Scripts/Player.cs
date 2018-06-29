using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float horizontalVelocity;
	public float jumpVelocity;
	public float fallMultiplier = 2.5f;
	private Animator animator;
	private Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>( );
		rb = GetComponent<Rigidbody2D>( );
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMove = Input.GetAxis( "Horizontal" ) * horizontalVelocity * Time.deltaTime;
		transform.Translate( horizontalMove, 0, 0 );

		if( Input.GetButtonDown( "Horizontal" ) ) {
			animator.SetBool( "playerRun", true );
		}
		else if( Input.GetButtonUp( "Horizontal" ) ) {
			animator.SetBool( "playerRun", false );
		}

		if( Input.GetButtonDown( "Jump" ) ) {
			rb.velocity = Vector2.up * jumpVelocity;
			animator.SetTrigger( "playerJump" );
		}

		if( rb.velocity.y < 0 ) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * ( fallMultiplier - 1 ) * Time.deltaTime;
			animator.SetTrigger( "playerFall" );
		}
	}
}

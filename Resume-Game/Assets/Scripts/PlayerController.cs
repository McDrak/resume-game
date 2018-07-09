using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
	public float jumpTakeOffSpeed = 7f;
	public float maxSpeed = 7f;

	private SpriteRenderer sr;
	private Animator animator;
	private bool canDoubleJump;

	void Awake( ) {
		sr = GetComponent<SpriteRenderer>( );
		animator = GetComponent<Animator>( );
		canDoubleJump = false;
	}

	protected override void ComputeVelocity( ) {
		Vector2 move = Vector2.zero;
		move.x = Input.GetAxis( "Horizontal" );

		if( Input.GetButtonDown( "Jump" ) && grounded ) {
			velocity.y = jumpTakeOffSpeed;
			canDoubleJump = true;
			animator.SetTrigger( "playerJump" );
		}
		else if ( Input.GetButtonDown( "Jump" ) && canDoubleJump ) {
			canDoubleJump = false;
			velocity.y = 0;
			velocity.y = jumpTakeOffSpeed;
			animator.SetTrigger( "playerSommer" );
		}

		bool flipSprite = ( sr.flipX ? ( move.x > 0.01f ) : ( move.x < -0.01f ) );
		if( flipSprite ) {
			sr.flipX = !sr.flipX;
		}

		animator.SetBool( "playerGrounded", grounded );
		animator.SetFloat( "playerVelocityX", Mathf.Abs( velocity.x ) / maxSpeed );

		targetVelocity = move * maxSpeed;
	}
}
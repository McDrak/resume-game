﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
	public float jumpTakeOffSpeed = 7f;
	public float maxSpeed = 7f;
	public Cinemachine.CinemachineVirtualCamera upperCam;
	public GameObject[ ] boxes = new GameObject[ 3 ];

	private SpriteRenderer sr;
	private Animator animator;
	private bool canDoubleJump;
	private Vector2 lastCheckpoint;
	private bool canRaise;
	private float collectibleCounter;

	void Awake( ) {
		sr = GetComponent<SpriteRenderer>( );
		animator = GetComponent<Animator>( );
		canDoubleJump = false;
		canRaise = true;
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
		animator.SetFloat( "playerVelocityY", velocity.y );

		targetVelocity = move * maxSpeed;
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.tag == "Spike" ) {
			animator.SetTrigger( "playerDeath" );
			StartCoroutine( respawnDelay( ) );
		}
		else if( other.tag == "Checkpoint" ) {
			lastCheckpoint = new Vector2( other.transform.position.x, other.transform.position.y );
		}
		else if( other.tag == "HeightMod" ) {
			if( canRaise ) {
				upperCam.gameObject.SetActive( true );
				canRaise = false;
			}
			else {
				upperCam.gameObject.SetActive( false );
				canRaise = true;
			}
		}
		else if( other.tag == "Collectible" ) {
			collectibleCounter++;
			other.GetComponent<Animator>( ).SetTrigger( "gemTaken" );
			Destroy( other.gameObject, 0.6f );

			if( collectibleCounter == 5 ) {
				for( int i = 0; i < boxes.Length; i++ ) {
					Destroy( boxes[ i ] );
				}
			}
		}
	}

	IEnumerator respawnDelay( ) {
		yield return new WaitForSeconds( 1.5f );
		upperCam.gameObject.SetActive( false );
		canRaise = true;
		transform.position = new Vector2( lastCheckpoint.x, lastCheckpoint.y );
	}
}
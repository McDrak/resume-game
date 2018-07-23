using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInfo : MonoBehaviour {
	public GameObject banner;
	public Text targetTxt;
	private string txt;
	public bool displayInfo;

	// Use this for initialization
	void Start () {
		displayInfo = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown( 0 ) ) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit2D hit = Physics2D.Raycast( ray.origin, ray.direction, Mathf.Infinity );

			if( hit && hit.collider != null ) {
				displayInfo = true;
				switch( hit.collider.name ) {
					case "C Icon":
						txt = "C Programming Language";
					break;

					case "C++ Icon":
						txt = "C++ Programming Language";
					break;

					case "C# Icon":
						txt = "C# Programming Language";
					break;

					case "Java Icon":
						txt = "Java Programming Language";
					break;

					case "Unity Icon":
						txt = "Unity 3D Engine";
					break;

					case "Unreal Icon":
						txt = "Unreal Engine";
					break;

					case "Ogre3D Icon":
						txt = "Ogre3D Graphics Rendering Engine";
					break;

					case "Python Icon":
						txt = "Python Programming Language";
					break;

					case "Typescript Icon":
						txt = "TypeScript Programming Language";
					break;

					case "Web Icon":
						txt = "HTML / CSS / JavaScript";
					break;

					case "TesisTitle": {
						Application.OpenURL( "https://github.com/mcdrak/tesis" );
						displayInfo = false;
					}
					break;
				}
			}
			else {
				displayInfo = false;
			}
		}
		ShowBanner( );
	}

	void ShowBanner( ) {
		if( displayInfo ) {
			targetTxt.text = txt;
			banner.SetActive( true );
		}
		else {
			banner.SetActive( false );
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject TitleMenu;
    public GameObject CameraStart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey) {
            TitleMenu.SetActive( false );
            CameraStart.GetComponent<Animator>().SetBool( "Pressedkey", true );
        }
	}
}

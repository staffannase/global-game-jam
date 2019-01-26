using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    AudioSource soundPlayer;
    Animator anim;
    public AudioClip steps0;
    public AudioClip steps1;
    public AudioClip steps2;

    public AudioClip Shoot0;
    public AudioClip Shoot1;
    public AudioClip Shoot2;

    // Use this for initialization
    void Start () {
        soundPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Random.InitState( 1234*4132*45 ); 
    }
	
	// Update is called once per frame
	public void PlayWalkSound () {
		if( anim.GetFloat("Move") > 0.1f && !soundPlayer.isPlaying && anim.GetBool("IsGrounded") ) {
            int r = Random.Range( 0, 3 );
            float p = Random.Range( 0.89f, 1.25f );
            soundPlayer.pitch = p;
            switch ( r ) {
                case 0:
                    soundPlayer.PlayOneShot( steps0 );
                    break;
                case 1:
                    soundPlayer.PlayOneShot( steps1 );
                    break;
                case 2:
                    soundPlayer.PlayOneShot( steps2 );
                    break;
                default:
                    break;

            }
        }
	}

    public void PlayFireSound() {
        int r = Random.Range( 0, 3 );
        float p = Random.Range( 0.89f, 1.25f );
        soundPlayer.pitch = p;
        switch ( r ) {
            case 0:
                soundPlayer.PlayOneShot( Shoot0 );
                break;
            case 1:
                soundPlayer.PlayOneShot( Shoot1 );
                break;
            case 2:
                soundPlayer.PlayOneShot( Shoot2 );
                break;
            default:
                break;

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicLeafEffect : MonoBehaviour {

    float lerpTime = 1.5f;
    float currentLerpTime;
    bool isLerp = false;

    Color LeafStartColor;
    Color LeafEndColor;

    public Transform magicEffect;
    Vector3 meStart;
    Vector3 meEnd;

    // Use this for initialization
    void Start () {
        LeafStartColor = this.GetComponent<SpriteRenderer>().color;
        LeafEndColor = new Color( this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.g, 0 );
        meStart = magicEffect.GetChild(0).localScale;
        meEnd = new Vector3( 160f, 160f, 160f );
        magicEffect.gameObject.SetActive( false );

    }
	
	// Update is called once per frame
	void Update () {
        if ( isLerp ) { 
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if ( currentLerpTime > lerpTime ) {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;
            this.GetComponent<SpriteRenderer>().color = Color.Lerp( LeafStartColor, LeafEndColor, perc );
            magicEffect.GetChild(0).localScale = Vector3.Lerp(meStart, meEnd, perc );
            if(Mathf.Approximately(perc, 1f ) ) {
                SceneManager.LoadScene( 1, LoadSceneMode.Single );
            }
        }
    }

    public void StartMagic() {
        isLerp = true;        
        magicEffect.gameObject.SetActive( true );
    }

}

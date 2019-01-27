using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLeafFall : MonoBehaviour {
    public GameObject leaf;

    //public Scen

    public void StartLeafFalling() {
        leaf.GetComponent<Animator>().SetBool( "StartFalling", true );
    }

    public void StartMagicEffect() {
        leaf.GetComponent<MagicLeafEffect>().StartMagic();
    }
}

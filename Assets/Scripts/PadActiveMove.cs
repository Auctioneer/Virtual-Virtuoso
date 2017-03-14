using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadActiveMove : MonoBehaviour
{
    //This class is firstly to set a temp variable to show whether the pad is being clicked while within the trigger
    //(Necessary for moving through scenes)
    //And secondly to do the grip movement
    //(Gotta keep it modular)

    //Boolean - is the pad being clicked down while in the object?
    bool padClickDown;

	// Use this for initialization
	void Start ()
    {
        padClickDown = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") == true)
        {

        }
    }
}

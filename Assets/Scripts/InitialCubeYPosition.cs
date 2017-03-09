using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCubeYPosition : MonoBehaviour {

    //To get position of head
    public GameObject eyeCamera;
    Vector3 eyeCameraTransPosition;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Get current position of user's head on the y axis
        eyeCameraTransPosition = eyeCamera.transform.position;
        float eyeCameraYPosition = eyeCameraTransPosition.y;

        //Vary start cube's y position relative to the user's head
        Vector3 cubePosition = new Vector3(1.005f, eyeCameraYPosition-0.3f, -0.099f);
        this.gameObject.transform.position = cubePosition;
    }


    public void clickedBehaviour()
    {
        //Set cube height with game manager
        GameObject manager = GameObject.Find("GameManager");
        manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - 0.3f);
    }
    
    IEnumerator beginSceneTransition()
    {
        //Wait a few seconds
        yield return new WaitForSeconds(3);

        //Tell GameManager to do scene change

        
    }
}

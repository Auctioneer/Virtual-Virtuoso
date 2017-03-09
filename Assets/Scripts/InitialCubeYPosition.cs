using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCubeYPosition : MonoBehaviour {

    //To get position of head
    public GameObject eyeCamera;
    Vector3 eyeCameraTransPosition;
    GameObject manager;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("GameManager");
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
        print("We're in clicked behaviour m8");

        //Set cube height with game manager
        manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - 0.3f);

        //Call method to move to next scene
        StartCoroutine(beginSceneTransition());
    }
    
    //Coroutine for changing scene
    IEnumerator beginSceneTransition()
    {
        //Wait a few seconds
        yield return new WaitForSeconds(1.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
        
    }
}

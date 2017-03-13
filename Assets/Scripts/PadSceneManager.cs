using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadSceneManager : MonoBehaviour
{

    //This class is for monitoring the progress of the cube scene, and telling the Game Manager to move scenes if the trigger is activated

    public GameObject padCube;
    GameObject manager;
    public GameObject leftController;
    public GameObject rightController;

    public GameObject onBox1;
    public GameObject onBox2;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");

        //Disable all actions except pad
        leftController.GetComponent<EasyController>().setPermissionsPadScene();
        rightController.GetComponent<EasyController>().setPermissionsPadScene();

        //Get the boxes in the background playing
        onBox1.GetComponent<CubeGlow>().Activate();
        onBox2.GetComponent<CubeGlow>().Activate();
    }

    // Update is called once per frame
    void Update()
    {
        //When the cube is temp activated, we can move on
        if (padCube.GetComponent<CubeGlow>().GetTempActive() == true)
        {
            StartCoroutine(beginTransition());
        }
    }

    IEnumerator beginTransition()
    {
        //Wait a few seconds
        //Bit longer than initial teleport because we wanna listen to the loop for a bit
        yield return new WaitForSeconds(4.0f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }
}
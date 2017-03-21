using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadySceneDelay : MonoBehaviour
{

    GameObject manager;


    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");
        StartCoroutine(beginTransition());
    }


    IEnumerator beginTransition()
    {
        //Wait a few seconds
        //Bit longer than initial teleport because we wanna listen to the loop for a bit
        yield return new WaitForSeconds(4.0f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }



    // Update is called once per frame
    void Update()
    {

    }
}

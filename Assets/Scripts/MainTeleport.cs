using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTeleport : MonoBehaviour
{

    public GameObject manager;

    public string nextScene;

    AudioSource soundPlayer;
    bool soundPlayed;

    // Use this for initialization
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayed = false;

        manager = GameObject.Find("GameManager");
    }

    //Coroutine for changing scene
    IEnumerator beginSceneTransition()
    {
        if (soundPlayed == false)
        {
            soundPlayer.Play();
            soundPlayed = true;
        }

        //Wait a few seconds
        yield return new WaitForSeconds(1.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();

    }

    //Walking into the teleporter trigger initiates scene transition
    public void teleportTriggered()
    { 
        //Call method to move to next scene
        StartCoroutine(beginSceneTransition());

    }
}

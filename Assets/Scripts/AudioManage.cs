using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour {

    public delegate void AudioBroadcast();
    public static event AudioBroadcast OnBroadcast;

	// Use this for initialization
	void Start ()
    {
        //Run broadcast every four seconds from start
        InvokeRepeating("broadcastAudio", 0.0f, 4.0f);
	}
	
	// Sends out a call to play audio if the object has been flagged to do so
	void broadcastAudio ()
    {
        if (OnBroadcast != null)
        {
            OnBroadcast();
        }
	}
}

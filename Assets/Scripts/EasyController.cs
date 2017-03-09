using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyController : MonoBehaviour {

    private SteamVR_TrackedController device;
    private AudioSource targetAudio;
    private GameObject objectTouching;


    //Soloing event
    public delegate void MuteBroadcast();
    public static event MuteBroadcast OnMute;

    public delegate void UnmuteBroadcast();
    public static event UnmuteBroadcast OnUnmute;


    // Use this for initialization
    void Start () {
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
        device.PadClicked += PadClick;
        device.PadUnclicked += PadUnClick;
        objectTouching = null;
	}
	
	// Make larger cube active
	void Trigger (object sender, ClickedEventArgs e)
    {
        print("Click!");

        
        if (objectTouching != null)
        {
            if (objectTouching.CompareTag("StartCube") == true)
            {
                objectTouching.GetComponent<InitialCubeYPosition>().clickedBehaviour();
            }

            //If it's a music cube, do the music behaviour
            else if (objectTouching.CompareTag("LoopCube") == true)
            {
                //Move all this below stuff to the cube's script
                objectTouching.GetComponent<CubeGlow>().SwitchActive();

                if (objectTouching.GetComponent<CubeGlow>().GetActive() == false)
                {
                    objectTouching.GetComponent<CubeGlow>().Deactivate();
                }
            }
        }
        //else if (objectTouching.CompareTag"")
    }

    //If within box, solos it
    //Only if we're at a certain stage though, so maybe do a check for this here?
    void PadClick(object sender, ClickedEventArgs e)
    {
        print("Pad click.");

        if (objectTouching != null)
        {
            MuteAllOthers();
        }
    }

    //Mute event
    void MuteAllOthers()
    {
        if (OnMute != null)
        {
            OnMute();
        }
    }

    void PadUnClick(object sender, ClickedEventArgs e)
    {
        print("Pad release.");
        UnMuteAll();
    }

    void UnMuteAll()
    {
        if (OnUnmute != null)
        {
            OnUnmute();
        }
    }

    //Entering cube zone
    private void OnTriggerEnter(Collider other)
    {
        print("Controller trigger entered.");
        objectTouching = other.gameObject;

        objectTouching.GetComponent<CubeGlow>().SetTempActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        print("Controller trigger exited.");
        objectTouching.GetComponent<CubeGlow>().SetTempActive(false);
        objectTouching = null;
    }
}

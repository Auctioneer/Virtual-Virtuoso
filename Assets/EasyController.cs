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
	
	// Update is called once per frame
	void Trigger (object sender, ClickedEventArgs e)
    {
        print("Click!");

        if (objectTouching != null)
        {
            objectTouching.GetComponent<CubeGlow>().SwitchActive();

            if (objectTouching.GetComponent<CubeGlow>().GetActive() == false)
            {
                objectTouching.GetComponent<CubeGlow>().Deactivate();
            }
        }
    }

    //If within box, solos it
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

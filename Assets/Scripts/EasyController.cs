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

    //Whether the functionality of a control is active or not
    bool triggerActive;
    bool padLeftActive;
    bool padRightActive;

    //To be decremented (maybe) when I get left and right pads working
    bool padActive;
    

    // Use this for initialization
    void Start ()
    {
        //Set up broadcasts and initial variables
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
        device.PadClicked += PadClick;
        device.PadUnclicked += PadUnClick;
        objectTouching = null;

        //Starts up with only trigger active
        triggerActive = true;
        padActive = false;
        padLeftActive = false;
        padRightActive = false;

	}
	
	//Trigger behaviour
	void Trigger (object sender, ClickedEventArgs e)
    {
        print("Click!");

        //If we can use the trigger
        if (triggerActive == true)
        {
            //Must be present to avoid null reference exception
            if (objectTouching != null)
            {
                //If we're clicking the start cube, get it to do its thing
                if (objectTouching.CompareTag("StartCube") == true)
                {
                    objectTouching.GetComponent<InitialCubeYPosition>().clickedBehaviour();
                }

                //If it's a music cube, do the music behaviour
                //else if (objectTouching.CompareTag("LoopCube") == true)
                else
                {
                    //Move all this below stuff to the cube's script
                    objectTouching.GetComponent<CubeGlow>().SwitchActive();

                    if (objectTouching.GetComponent<CubeGlow>().GetActive() == false)
                    {
                        objectTouching.GetComponent<CubeGlow>().Deactivate();
                    }
                }
            }

        }
    }

    //If within box, solos it
    //Only if we're at a certain stage though, so maybe do a check for this here?
    void PadClick(object sender, ClickedEventArgs e)
    {
        //If we can use the pad
        if (padActive == true)
        {
            print("Pad click.");

            if (objectTouching != null)
            {
                MuteAllOthers();
            }
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
        print(objectTouching.tag);

        if (objectTouching.CompareTag("Teleporter") == true)
        {
            objectTouching.GetComponent<StartTeleporterTrigger>().teleportTriggered();
        }
        else
        {
            objectTouching.GetComponent<CubeGlow>().SetTempActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Controller trigger exited.");
        if (objectTouching.CompareTag("Teleporter") == true)
        {

        }
        else
        {
            objectTouching.GetComponent<CubeGlow>().SetTempActive(false);
        }
        objectTouching = null;
    }

    //PERMISSION SETTING FOR EACH SCENE
    public void setPermissionsTriggerScene()
    {
        triggerActive = true;
        padActive = false;
    }

    public void setPermissionsPadScene()
    {
        triggerActive = false;
        padActive = true;
    }

    public void setPermissionsFullFunctionality()
    {
        triggerActive = true;
        padActive = true;
    }
}

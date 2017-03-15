using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyController : MonoBehaviour {

    private SteamVR_TrackedController device;
    private AudioSource targetAudio;
    public GameObject objectTouching;


    //Soloing event
    public delegate void MuteBroadcast();
    public static event MuteBroadcast OnMute;

    public delegate void UnmuteBroadcast();
    public static event UnmuteBroadcast OnUnmute;

    //Whether the functionality of a control is active or not
    bool triggerActive;
    public bool padTopActive;
    public bool padBottomActive;

    //To be decremented (maybe) when I get left and right pads working
    bool padActive;

    //Touchpad co-ordinate vector
    public Vector2 touchpadCoordinates;


    // Use this for initialization
    void Start ()
    {
        //Set up broadcasts and initial variables
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
        device.PadClicked += PadClick;
        device.PadUnclicked += PadUnClick;
        objectTouching = null;

        //Change dis
        device.Gripped += grab;
        device.Ungripped += releaseGrab;

        //Starts up with only trigger active
        triggerActive = true;
        padActive = false;
        padBottomActive = false;
        padTopActive = false;

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
        //Get co-ordinates of where the pad is being pressed
        //Thankfully, the object e already stores them, how about that!
        touchpadCoordinates = new Vector2(e.padX, e.padY);

        //Depending on what part of the pad is clicked, a different action will be performed. Scale is -1 to 1 bottom to top
        //For now, I'll say that the top part is for moving blocks, and the bottom part is for soloing them
       // if (padActive == true)
       // {
          //  print("Pad click at co-ordinates" + touchpadCoordinates);
          //  print(touchpadCoordinates.y);
       // }

        //SOLOING FUNCTIONALITY
        //If the user is touching the below area and that area is active
        if (padBottomActive == true && touchpadCoordinates.y < 0)
        {
            print(touchpadCoordinates.y);

            //If they're touching a musical block
            if (objectTouching != null)
            {
                //Mute all the other blocks (apart from those marked as temp active
                MuteAllOthers();
            }
        }

        //GRABBING/MOVING FUNCTIONALITY
        else if (padTopActive == true && touchpadCoordinates.y > 0)
        {

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
        padBottomActive = true;
        padTopActive = false;
    }

    public void setPermissionsMoveScene()
    {

    }

    public void setPermissionsFullFunctionality()
    {
        triggerActive = true;
        padActive = true;
    }


    //GRAB ACTION CURRENTLY ON GRIP BUT WE'LL CHANGE IT
    //JUST TO MAKE IT EASIER FOR ME MAN
    public void grab(object sender, ClickedEventArgs e)
    {
        if ((objectTouching != null) && objectTouching.CompareTag("LoopCube") == true)
        {
            objectTouching.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    //Letting go of grip
    public void releaseGrab(object sender, ClickedEventArgs e)
    {
        if (objectTouching != null)
        {
            objectTouching.gameObject.transform.parent = null;
        }
    }
}

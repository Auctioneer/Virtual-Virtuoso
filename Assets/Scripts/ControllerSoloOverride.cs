using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSoloOverride : MonoBehaviour
{

    private SteamVR_TrackedController device;
    private AudioSource targetAudio;
    public GameObject objectTouching;

    //Soloing event
    public delegate void MuteBroadcast();
    public static event MuteBroadcast OnMute;

    public delegate void UnmuteBroadcast();
    public static event UnmuteBroadcast OnUnmute;

    //Touchpad co-ordinate vector
    public Vector2 touchpadCoordinates;

    // Use this for initialization
    void Start()
    {
        //Set up broadcasts and initial variables
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
        device.PadClicked += PadClick;
        device.PadUnclicked += PadUnClick;
        objectTouching = null;

    }

    //Trigger behaviour
    void Trigger(object sender, ClickedEventArgs e)
    {
        print("Click!");

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
    
    //If within box, solos it
    //Only if we're at a certain stage though, so maybe do a check for this here?
    void PadClick(object sender, ClickedEventArgs e)
    {
        //Get co-ordinates of where the pad is being pressed
        //Thankfully, the object e already stores them, how about that!
        touchpadCoordinates = new Vector2(e.padX, e.padY);
        print(touchpadCoordinates.y);

        //Depending on what part of the pad is clicked, a different action will be performed. Scale is -1 to 1 bottom to top

        //SOLOING FUNCTIONALITY
        //If the user is touching the below area and that area is active
        if (touchpadCoordinates.y < 0)
        {
            //If they're touching a musical block
            if (objectTouching != null)
            {
                //Mute all the other blocks (apart from those marked as temp active
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
        else if (objectTouching.CompareTag("LoopCube") == true || objectTouching.CompareTag("MusicCube") == true)
        {
            objectTouching.GetComponent<CubeGlow>().SetTempActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Controller trigger exited.");
        if (objectTouching.CompareTag("LoopCube") == true || objectTouching.CompareTag("MusicCube") == true)
        {
            objectTouching.GetComponent<CubeGlow>().SetTempActive(false);
        }
        objectTouching = null;
    }
}

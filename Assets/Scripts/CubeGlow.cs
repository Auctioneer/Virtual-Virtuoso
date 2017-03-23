using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGlow : MonoBehaviour
{
    public GameObject cubeSelected;
    private AudioSource audioPlayer;
    GameObject manager;

    //Height of cube when spawned
    float initialCubeHeight;

    //Flag for whether it should play constantly
    private bool isActivated;

    //Flag for playing while highlighted
    private bool tempActive;

    //This is what colour the box will change to after we highlight it for the first time
    public Material afterHighlightMaterial;

    // Use this for initialization
    void Start ()
    {
        audioPlayer = GetComponent<AudioSource>();
        isActivated = false;
        audioPlayer.Play();
        audioPlayer.mute = true;
        tempActive = false;
        manager = GameObject.Find("GameManager");

        //Get height from game manager
        initialCubeHeight = manager.GetComponent<GameManager>().getCubeHeight();

        //Set height for cube
        Vector3 temp = new Vector3(0, initialCubeHeight, 0);
        this.gameObject.transform.Translate(temp);
    }

    private void OnEnable()
    {
        EasyController.OnMute += MuteAllExcept;
        EasyController.OnUnmute += UnMute;
    }

    private void OnDisable()
    {
        EasyController.OnMute -= MuteAllExcept;
        EasyController.OnUnmute -= UnMute;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") == true)
        {
            cubeSelected.SetActive(true);

            //Now that the user has 'discovered' the loop sound, we'll change the original box's colour to match
            //I used to do this with instrument types but instead we can just change the material (default red)
            if (afterHighlightMaterial != null)
            {
                GetComponent<Renderer>().material = afterHighlightMaterial;
            }

            SetAudioLoud();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("GameController") == true) && (isActivated == false))
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        cubeSelected.SetActive(false);
        MuteAudio();
    }

    public void Activate()
    {
        cubeSelected.SetActive(true);
        SetAudioLoud();
    }

    //I don't think I actually use this
    public void ChangeAudioVolume()
    {
        if (audioPlayer.volume == 0.0f)
        {
            audioPlayer.volume = 0.124f;
        }
        else
        {
            audioPlayer.volume = 0.0f;
        }
    }

    
    void SetAudioLoud()
    {
        //audioPlayer.volume = 0.124f;

        //EXPERIMENTAL
        //audioPlayer.volume = 1.0f;

        //EVEN MORE EXPERIMENTAL
        audioPlayer.mute = false;
    }

    public void UnMute()
    {
        if (isActivated == true)
        {
            //audioPlayer.volume = 0.124f;
            SetAudioLoud();
        }
    }

    void MuteAllExcept()
    {
        if (tempActive == false)
        {
            MuteAudio();
        }
    }

    void MuteAudio()
    {
        //audioPlayer.volume = 0.0f;
        audioPlayer.mute = true;
    }

    public void SwitchActive()
    {
        isActivated = !isActivated;
    }

    public bool GetActive()
    {
        return isActivated;
    }

    public void SetTempActive(bool temp)
    {
        tempActive = temp;
    }

    public bool GetTempActive()
    {
        return tempActive;
    }

    public float GetVolume()
    {
        float currentVolume = audioPlayer.volume;

        return currentVolume;
    }

    public void SetVolume(float volume)
    {
        audioPlayer.volume = volume;
    }

}

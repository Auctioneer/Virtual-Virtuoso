using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGlow : MonoBehaviour
{
    public GameObject cubeSelected;
    private AudioSource audioPlayer;
    public GameObject manager;

    //Height of cube when spawned
    float initialCubeHeight;

    //Flag for whether it should play constantly
    private bool isActivated;

    //Flag for playing while highlighted
    private bool tempActive;

	// Use this for initialization
	void Start ()
    {
        audioPlayer = GetComponent<AudioSource>();
        isActivated = false;
        audioPlayer.Play();
        tempActive = false;
        manager = GameObject.Find("GameManager");

        //Get height from game manager
        initialCubeHeight = manager.GetComponent<GameManager>().getCubeHeight();

        //Set height for cube


        Vector3 temp = new Vector3(0, initialCubeHeight, 0);
        this.gameObject.transform.Translate(temp);
        //this.gameObject.transform.position += temp;

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
        audioPlayer.volume = 0.124f;
    }

    void UnMute()
    {
        if (isActivated == true)
        {
            audioPlayer.volume = 0.124f;
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
        audioPlayer.volume = 0.0f;
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

}

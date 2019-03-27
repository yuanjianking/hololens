using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Holoville.HOTween;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;
using UnityEngine.EventSystems;

public class Start : MonoBehaviour, IInputClickHandler
{
	
    public AudioClip MenuSound;//The Sound Played when you click on the play button
    public string _NextScene;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GetComponent<AudioSource>().PlayOneShot(MenuSound);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.001f);
        SceneManager.LoadScene(_NextScene);
    }
    
}
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour, IInputClickHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        int x = int.Parse(transform.name);
        int y = int.Parse(transform.parent.name);
        ChessManager.GetInstant().CheckAndMove(new PointData(x, y));
        //隐藏路线
        ChessManager.GetInstant().HidenRoad();
        eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayGame1 : MonoBehaviour {

    private SteamVR_TrackedController device;
	// Use this for initialization
	void Start () {
        device.GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
	}
	
	void Trigger (object sender, ClickedEventArgs e)
    {
        Debug.Log("Scenai vajadzetu nomainities");
        SteamVR_LoadLevel.Begin("LevelToLoad");
    }

}

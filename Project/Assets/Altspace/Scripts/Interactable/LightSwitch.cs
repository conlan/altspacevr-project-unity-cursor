using UnityEngine;
using System.Collections;

public class LightSwitch : Interactable {
	public Light lightSource;

	// Use this for initialization
//	void Start () {}
	
	// Update is called once per frame
//	void Update () {}

	public override void Interact() {
		// toggle the light on/off
		this.lightSource.enabled = !this.lightSource.enabled;
	}
}
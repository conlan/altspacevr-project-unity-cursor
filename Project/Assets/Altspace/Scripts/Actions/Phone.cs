﻿using UnityEngine;
using System.Collections;

public class Phone : Actionable {
	public bool isRinging;

	// Use this for initialization
	void Start() {
		if (this.isRinging) {
			this.StartRinging();
		} else {
			this.StopRinging();
		}
	}

	public override void Use() {
		this.isRinging = !this.isRinging;

		if (this.isRinging) {
			this.StartRinging();
		} else {
			this.StopRinging();

			this.AnswerPhone();
		}
	}

	public void AnswerPhone() {
		// create a phone call instance for the player
		Vector3 phoneCallPosition = Camera.main.transform.position + Camera.main.transform.forward.normalized * 1.0f;
		
		GameObject gameObj = Instantiate(PrefabManager.Instance.phoneCallPrefab, phoneCallPosition, Quaternion.identity) as GameObject;
		// have the object face the camera directly
		gameObj.transform.LookAt(Camera.main.transform);
		// now attach it to the camera
		gameObj.transform.parent = Camera.main.transform;
		// grab the phone call script
		PhoneCall phoneCall = gameObj.GetComponent<PhoneCall>();
		// present a message!
		phoneCall.Present("Knock knock, Neo...");
	}

	public void StopRinging() {
		this.audio.Stop();

		this.actionName = "Call";
	}

	public void StartRinging() {
		this.audio.Play();

		this.actionName = "Answer";
	}
}
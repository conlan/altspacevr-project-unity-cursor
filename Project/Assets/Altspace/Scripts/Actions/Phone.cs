using UnityEngine;
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
		}
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
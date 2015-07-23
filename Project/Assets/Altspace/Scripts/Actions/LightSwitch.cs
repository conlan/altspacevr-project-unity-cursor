using UnityEngine;
using System.Collections;

public class LightSwitch : Actionable {
	public Light lightSource;

	void Start() {
		if ((this.actionName == null) || (this.actionName.Length == 0)) {
			this.UpdateActionName();
		}
	}

	private void UpdateActionName() {
		if (this.lightSource.enabled) {
			this.actionName = "Turn Off";
		} else {
			this.actionName = "Turn On";
		}
	}

	public override void Use() {
		// toggle the light on/off
		this.lightSource.enabled = !this.lightSource.enabled;

		this.UpdateActionName();
	}
}
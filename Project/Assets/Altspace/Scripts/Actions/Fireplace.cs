using UnityEngine;
using System.Collections;

public class Fireplace : Actionable {
	public GameObject flame;

	// Use this for initialization
	void Start () {
		this.UpdateActionName();
	}

	private void UpdateActionName() {
		if (this.flame.activeSelf) {
			this.actionName = "Put Out";
		} else {
			this.actionName = "Start Fire";
		}
	}

	public override void Use() {
		this.flame.gameObject.SetActive(!this.flame.gameObject.activeSelf);

		this.UpdateActionName();
	}
}
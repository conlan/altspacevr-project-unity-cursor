using UnityEngine;
using System.Collections;

public class Piano : Actionable {
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		if ((this.actionName == null) || (this.actionName.Length == 0)) {
			this.actionName = "Play";
		}
	}
	
	public override void Use() {

		AudioSource.PlayClipAtPoint(clip, this.transform.position);
	}
}

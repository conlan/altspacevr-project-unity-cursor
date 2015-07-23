using UnityEngine;
using System.Collections;

public class Translatable : MonoBehaviour {
	private float startTime = -1;

	private float duration;

	private Vector3 targetWorldPosition;

	private Vector3 startWorldPosition;

	// Use this for initialization
	void Start () {	
	}

	public void TranslateTo(Vector3 worldPosition, float duration) {
		this.startWorldPosition = this.transform.position;
		this.targetWorldPosition = worldPosition;

		this.duration = duration;

		this.startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.startTime > 0) {
			// check how much time has elapsed
			float elapsed = Time.time - this.startTime;
			// normalize it to 0 -> 1.0
			float t = Mathf.Min(elapsed / duration, 1.0f);
			// lerp towards the target position
			this.transform.position = Vector3.Lerp(this.startWorldPosition, this.targetWorldPosition, t);
			// check if we're done
			if (t >= 1.0f) {
				this.startTime = -1;
			}
		}
	}
}
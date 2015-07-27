using UnityEngine;
using System.Collections;

public class Scalable : MonoBehaviour {
	private float startTime = -1;

	private float duration;

	private Vector3 targetScale;
	
	private Vector3 startScale;

	// Use this for initialization
	void Start () {
	
	}

	public void SetScale(Vector3 newScale) {
		this.transform.localScale = newScale;
	}

	public void ScaleToSize(Vector3 targetScale, float duration) {
		this.startScale = this.transform.localScale;

		this.targetScale = targetScale;

		this.duration = duration;

		this.startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.startTime >= 0) {
			// check how much time has elapsed
			float elapsed = Time.time - this.startTime;
			// normalize it to 0 -> 1.0
			float t = Mathf.Min(elapsed / duration, 1.0f);
			// lerp towards the target position
			this.transform.localScale = Vector3.Lerp(this.startScale, this.targetScale, t);
			// check if we're done
			if (t >= 1.0f) {
				this.startTime = -1;
			}
		}
	}
}
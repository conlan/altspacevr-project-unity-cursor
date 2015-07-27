using UnityEngine;
using System.Collections;

public class PhoneCall : MonoBehaviour {
	public MeshRenderer contactRenderer;

	public TextMesh textRenderer;

	public string fullMessage;
	private int messageIndex;
	private float nextMessageLetterTime = -1;

	// Use this for initialization
	void Start () {
	}

	public void Present(string message) {
		float presentDuration = 0.15f;

		this.fullMessage = message;
		this.messageIndex = 0;
		this.nextMessageLetterTime = Time.time + presentDuration;

		// scale in the contact texture
		Scalable scalable = contactRenderer.GetComponent<Scalable>();
		scalable.ScaleToSize(new Vector3(0.03f, 1, 0.03f), presentDuration);
		// play the answer phone audio clip
		AudioSource.PlayClipAtPoint(PrefabManager.Instance.answerPhoneClip, Vector3.zero);
	}

	void Update () {
		// animate the message 1 letter at a time
		if ((nextMessageLetterTime >= 0) && (Time.time >= nextMessageLetterTime)) {
			this.messageIndex += 1;

			this.textRenderer.text = this.fullMessage.Substring(0, this.messageIndex);
			// check if we're done
			if (this.messageIndex >= this.fullMessage.Length) {
				this.nextMessageLetterTime = -1;
				// destroy this message in a few seconds
				Destroy(this.gameObject, 3.0f);
			} else {
				this.nextMessageLetterTime = Time.time + 0.03f;
			}
		}
	}
}

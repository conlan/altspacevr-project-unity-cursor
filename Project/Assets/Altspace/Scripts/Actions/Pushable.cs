using UnityEngine;
using System.Collections;

public class Pushable : Actionable {
	public Rigidbody pushableRigidBody;

	void Start() {
		if ((this.actionName == null) || (this.actionName.Length == 0)) {
			this.actionName = "Push";
		}
	}
	
	public override void Use() {
		this.pushableRigidBody.AddForce(Vector3.up * Random.Range(3, 6), ForceMode.Impulse);

		Vector3 forwardDirection = this.pushableRigidBody.transform.position - Camera.main.transform.position;

		this.pushableRigidBody.AddForce(forwardDirection.normalized * Random.Range(3, 6), ForceMode.Impulse);
	}
}

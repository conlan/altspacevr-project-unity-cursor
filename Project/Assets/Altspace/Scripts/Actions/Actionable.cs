using UnityEngine;
using System.Collections;

public class Actionable : MonoBehaviour {
	public string actionName;

	public virtual void Use() {
		Debug.Log("Interact");
	}
	
	// Update is called once per frame
//	void Update () {}
}
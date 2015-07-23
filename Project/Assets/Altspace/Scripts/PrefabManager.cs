using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {
	private static PrefabManager singleton;

	public GameObject menuButtonPrefab;

	public AudioClip createMenuClip;

	public static PrefabManager Instance {
		get
		{
			if (singleton == null) {
				singleton = (PrefabManager) FindObjectOfType(typeof(PrefabManager));
			}

			return singleton;
		}
	}
}

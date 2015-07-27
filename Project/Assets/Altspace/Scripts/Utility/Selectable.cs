using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	public static Selectable CurrentHighlight { get; set; }

	public static Selectable CurrentSelected { get; set; }

	public Material NormalMaterial;
	public Material HighlightMaterial;

	private MeshRenderer[] meshRenderers;

	public Actionable[] actions;

	private ArrayList actionButtons;

	void Start()
	{
		this.meshRenderers = GetComponentsInChildren<MeshRenderer>();
	}

	void Update () {
		// if this selectable is the current selection
		if (this == CurrentHighlight)
		{
			if (meshRenderers[0].sharedMaterial != HighlightMaterial)
			{
				foreach (var renderer in meshRenderers)
				{
					renderer.sharedMaterial = HighlightMaterial;
				}
			}

			// check if the player clicks while highlighting a selection
			if (Input.GetButtonDown("Fire1")) {
				if (Selectable.CurrentSelected == this) {
					Selectable.CurrentSelected.DestroyMenu(true);
					Selectable.CurrentSelected = null;
				} else {
					if (Selectable.CurrentSelected != null) {
						Selectable.CurrentSelected.DestroyMenu(true);
					}
					
					Selectable.CurrentSelected = this;
					
					if (this.actions != null) {
						this.CreateMenu();
					}
				}
			}
		}
		else
		{
			if (meshRenderers[0].sharedMaterial != NormalMaterial)
			{
				foreach (var renderer in meshRenderers)
				{
					renderer.sharedMaterial = NormalMaterial;
				}
			}
		}
	}

	public void DestroyMenu(bool animated) {
		float menuSetupDuration = 0.05f;

		if (this.actionButtons != null) {
			foreach (MenuButton button in this.actionButtons) {
				if (animated) {
					button.collider.enabled = false;

					button.translatable.TranslateTo(this.transform.position, menuSetupDuration);

					Destroy(button.gameObject, menuSetupDuration);
				} else {
					Destroy(button.gameObject);
				}
			}
			this.actionButtons = null;
		}
	}

	public void CreateMenu() {
		this.actionButtons = new ArrayList();

		float menuSetupDuration = 0.05f;
		// calculate the direction to the camera from here
		Vector3 directionToCamera = Camera.main.transform.position - this.transform.position;
		// calculate the distance
		float distanceToCamera = Vector3.Distance(Camera.main.transform.position, this.transform.position);
		// find the target position for this menu (distance to the camera - 2 pts, but at least halfway towards the camera)
		Vector3 targetPositionCenter = this.transform.position + directionToCamera.normalized * Mathf.Max(0.5f, (distanceToCamera - 2));
		// play the create menu clip
		AudioSource.PlayClipAtPoint(PrefabManager.Instance.createMenuClip, Vector3.zero);
		// determine the starting menu button position offset, based on how many actions we'll show
		float targetPositionOffset = -(this.actions.Length - 1) / 2.0f;

		for (int i = 0; i < this.actions.Length; i++) {
			Actionable action = this.actions[i];

			GameObject menuButton = Instantiate(PrefabManager.Instance.menuButtonPrefab, this.transform.position, Quaternion.identity) as GameObject;

			MenuButton menuButtonRef = menuButton.GetComponent<MenuButton>();

			menuButtonRef.SetAction(action);
			// place the menu buttons from bottom to op
			Vector3 targetPosition = targetPositionCenter + (targetPositionOffset * 0.2f) * Vector3.up;
			// move the menu button from here to it's target position to create a trail
			menuButtonRef.translatable.TranslateTo(targetPosition, menuSetupDuration);
			// save the script reference
			actionButtons.Add(menuButtonRef);
			// bump the next menu position upwards
			targetPositionOffset += 1;
		}
	}
}
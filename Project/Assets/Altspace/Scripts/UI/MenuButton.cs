using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	public static MenuButton CurrentHovered { get; set; }

	public MeshRenderer backgroundRenderer;

	public TextMesh textRenderer;

	public Actionable action;

	public Translatable translatable;

	public Material backgroundMaterialNormal;
	public Material backgroundMaterialHovered;

	// Use this for initialization
	void Start () {
		// always stay aimed at the player
		this.transform.LookAt(Camera.main.transform);
	}

	void Update () {
		// always stay aimed at the player
		this.transform.LookAt(Camera.main.transform);

		if (this == CurrentHovered) {
			if (MouseLook.isLooking == false) {
				if (Input.GetButtonDown("Fire1")) {
					this.action.Use();

					if (Selectable.CurrentSelected != null) {
						Selectable.CurrentSelected.DestroyMenu(false);

						Selectable.CurrentSelected = null;
					}

					MenuButton.CurrentHovered = null;
				}
			}
		}
	}

	public void SetAction(Actionable action) {
		this.action = action;

		this.textRenderer.text = action.actionName;
	}

	public void SetHovered(bool hovered) {
		if (hovered) {
			this.backgroundRenderer.material = this.backgroundMaterialHovered;
		} else {
			this.backgroundRenderer.material = this.backgroundMaterialNormal;
		}
	}

	public void OnMouseOver() {
		this.SetHovered(true);
	}

	public void OnMouseExit() {
		this.SetHovered(false);
	}
}

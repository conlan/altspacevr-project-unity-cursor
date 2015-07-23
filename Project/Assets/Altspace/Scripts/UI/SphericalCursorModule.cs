using UnityEngine;
using System.Linq;

public class SphericalCursorModule : MonoBehaviour {
	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;
	
	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
//	private const int ColliderMask = (1 << 8);

	public LayerMask colliderMask;

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 20.0f;

    void Awake() {
		Cursor = transform.Find("Cursor").gameObject;

		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
        CursorMeshRenderer.renderer.material.color = new Color(0.0f, 0.8f, 1.0f);
    }	

	void Update() {
		// check if we're mouse looking...
		if (MouseLook.isLooking) {
			// if so, then disable the cursor for now
			CursorMeshRenderer.enabled = false;
			// and reset the selection
			Selectable.CurrentHighlight = null;

			MenuButton.CurrentHovered = null;
		} else {
			// re-enable the cursor incase we were just mouse looking
			CursorMeshRenderer.enabled = true;

			// Handle mouse movement to update cursor position.
			// Get the mouse inputs and apply the sensitivity
			float mouseX = Input.GetAxis("Mouse X") * this.Sensitivity;
			float mouseY = Input.GetAxis("Mouse Y") * this.Sensitivity;
			// Get the distance to the cursor
			float distanceToCursor = Vector3.Distance(this.transform.position, this.Cursor.transform.position);
			// Update the cursor using its distance as a modifer to ensure it moves at same speed when far away
			this.Cursor.transform.Translate(new Vector3(mouseX * distanceToCursor, mouseY * distanceToCursor, 0));
			// get the direction from us to the cursor
			Vector3 cursorDirection = (this.Cursor.transform.position - this.transform.position);
			// normalize the direction
			cursorDirection.Normalize();
			// Perform ray cast to find object cursor is pointing at.
			RaycastHit[] cursorHits = Physics.RaycastAll(this.transform.position, cursorDirection, SphericalCursorModule.MaxDistance, this.colliderMask);
//			                                             SphericalCursorModule.ColliderMask);	
			RaycastHit cursorHit = new RaycastHit();	

			// find the closest cursor hit to us
			float cursorHitPointDistance = float.MaxValue;
			// if we got a hit...
			if (cursorHits.Length > 0) {
				foreach (RaycastHit hit in cursorHits) {
					float dist = Vector3.Distance(hit.point, this.transform.position);
					if (dist < cursorHitPointDistance) {
						cursorHit = hit;
						cursorHitPointDistance = dist;
					}
				}
			}

			// Update highlighted object based upon the raycast.
			// If the cursor hit an object
			if (cursorHit.collider != null) {
				if (MenuButton.CurrentHovered != null) {
					MenuButton.CurrentHovered.SetHovered(false);
				}

				// place the cursor at the hit point position
				this.Cursor.transform.position = cursorHit.point;
				// set the cursor scale based on the distance to the hit point
				float cursorScale = (cursorHitPointDistance * this.DistanceScaleFactor + 1.0f) / 2.0f;
				this.Cursor.transform.localScale = new Vector3(cursorScale, cursorScale, cursorScale);
				// update the current selection
				MenuButton menuButton = cursorHit.collider.GetComponent<MenuButton>();

				if (menuButton != null) {
					Selectable.CurrentHighlight = null;

					MenuButton.CurrentHovered = menuButton;

					menuButton.SetHovered(true);
				} else {
					MenuButton.CurrentHovered = null;

					Selectable selectable = cursorHit.collider.GetComponent<Selectable>();

					Selectable.CurrentHighlight = selectable;
				}
			}
			else {
				// we didn't hit anything, so let's put the cursor on the surface of a virtual sphere around the player
				// first position it at our origin
				this.Cursor.transform.position = this.transform.position;
				// aim it in the direction from us to the cursor
				this.Cursor.transform.forward = cursorDirection;
				// place it on the surface of the sphere
				this.Cursor.transform.Translate(Vector3.forward * SphereRadius);
				// revert the scale to default
				this.Cursor.transform.localScale = this.DefaultCursorScale;
				// reset the current selection
				Selectable.CurrentHighlight = null;

				if (MenuButton.CurrentHovered != null) {
					MenuButton.CurrentHovered.SetHovered(false);

					MenuButton.CurrentHovered = null;
				}
			}
		}
	}
}

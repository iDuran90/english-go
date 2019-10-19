using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Components;
using UnityEngine;
using UnityEngine.UI;

public class DraggableItemManager : MonoBehaviour {
	public SlotItemManager[] avalaibleSlots;
	public SlotItemManager correctSlot;

	private Vector2 initialPosition;
	private float initialWidth;
	private float initialHeight;
	private float deltaX, deltaY;
	private bool allowMovement;

	public bool locked;

	void Start () {
		initialPosition = transform.position;
		initialWidth = ((RectTransform)transform).rect.width;
		initialHeight = ((RectTransform) transform).rect.height;
	}

	public void BackToInitialPosition() {
		transform.position = new Vector3(initialPosition.x, initialPosition.y);
		locked = false;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			allowMovement = IsTouchingElement(Input.mousePosition) & !locked; 

			if (allowMovement)
			{
				deltaX = Input.mousePosition.x - transform.position.x;
				deltaY = Input.mousePosition.y - transform.position.y;
			}
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
			allowMovement = false;

			SlotItemManager slotToUse = GetNearestSlot();
				
				if (slotToUse != null) {
					transform.position = new Vector3(slotToUse.transform.position.x, slotToUse.transform.position.y);
					locked = true;
					slotToUse.isTaken = true;
					slotToUse.BackToNormal();
				} else if (!locked) {
					BackToInitialPosition();
				}
		}
		
		if (allowMovement)
		{
			transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			transform.position = new Vector3(Input.mousePosition.x - deltaX, Input.mousePosition.y - deltaY);
			SlotItemManager slotToUse = GetNearestSlot();
			if (slotToUse != null) {
				slotToUse.ScaleSlot();
			} else {
				foreach (SlotItemManager slot in avalaibleSlots) {
					slot.BackToNormal();
				}
			}
		}
	}

	private bool IsTouchingElement(Vector3 touchPosition) {
		float widthMaxOffset = initialWidth / 2;
		float heightMaxOffset = initialHeight / 2;

		float widthOffset = Mathf.Abs(touchPosition.x - initialPosition.x);
		float heightOffset = Mathf.Abs(touchPosition.y - initialPosition.y);

		return widthOffset < widthMaxOffset && heightOffset < heightMaxOffset;
	}

	private SlotItemManager GetNearestSlot() {
		SlotItemManager nearestSlot = null;

		foreach (SlotItemManager slot in avalaibleSlots) {
			if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 50f &&
			    Mathf.Abs(transform.position.y - slot.transform.position.y) <= 30f && !slot.isTaken) {
				nearestSlot = slot;
			}
		}

		return nearestSlot;
	}
}

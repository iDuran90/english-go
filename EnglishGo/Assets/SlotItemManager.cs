using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemManager : MonoBehaviour {
	public bool isTaken;

	public void ScaleSlot() {
		transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}

	public void BackToNormal() {
		transform.localScale = new Vector3(1f, 1f, 1f);
	}
}

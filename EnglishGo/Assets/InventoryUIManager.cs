using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {
	public MenuUIManager menuUIMngr;
	
	private Color collectedItemColor =  new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f);

	public void OnOpenBtnClicked() {
		menuUIMngr.gameObject.SetActive(false);
		gameObject.SetActive(true);
	}
	
	public void OnCloseBtnClicked() {
		gameObject.SetActive(false);
		menuUIMngr.gameObject.SetActive(true);
	}
	
	private void OnEnable() {
		List<InventoryItem> items = gameObject.GetComponentsInChildren<InventoryItem>().ToList();
		
		foreach (var item in items) {
			int bookIdx = GameManager.Instance.CurrentPlayer.inventory.books.FindIndex(x => x.id == item.bookId);
			int parchmentIdx = GameManager.Instance.CurrentPlayer.inventory.books[bookIdx].parchments.FindIndex(x => x.id == item.parchmentId);

			if (GameManager.Instance.CurrentPlayer.inventory.books[bookIdx].parchments[parchmentIdx].collected) {
				Image btnImg = item.GetComponent<Image>();

				btnImg.color = collectedItemColor;
			}
		}
	}
}

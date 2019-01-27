using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
	[System.Serializable]
	public struct AmmoTypeStruct
	{
		public InventoryController.ProjectileType type;
		public Sprite sprite;
	}

	[SerializeField] private AmmoTypeStruct[] _ammos;
	[SerializeField] private Sprite _default;
	
	private InventoryController inventoryController;
	
	void Start () {
		inventoryController = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
		GetComponent<Image>().sprite = _default;
	}
	
	void Update () {
		InventoryController.ProjectileType type = inventoryController.getAmmoType();
		foreach (AmmoTypeStruct ammoTypeStruct in _ammos)
		{
			if (ammoTypeStruct.type == type)
			{
				GetComponent<Image>().sprite = ammoTypeStruct.sprite;
				break;
			}
		}

		GetComponentInChildren<Text>().text = inventoryController.getAmmoCount().ToString();
	}
}

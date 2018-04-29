using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StoreTabSelector : MonoBehaviour{
	public int type;
	public GameObject traps;
	public GameObject weapArmo;
	public GameObject items;

	public Button btnLoad;

	public void Start()
	{
		
		/*Navigation navigation = btnLoad.navigation;

		navigation.selectOnRight = btnLoad.OnSelect();


		btnLoad.navigation = navigation;*/
	}
	/*public void OnPointerEnter(BaseEventData eventData)
	{
		//do your stuff when selected
		if (type == 1) {
			traps.SetActive (true);
			weapArmo.SetActive (false);
			items.SetActive (false);
		}
		else if (type == 2) {
			traps.SetActive (false);
			weapArmo.SetActive (true);
			items.SetActive (false);
		} 
		else {
			traps.SetActive (false);
			weapArmo.SetActive (false);
			items.SetActive (true);
		}

	}*/

}

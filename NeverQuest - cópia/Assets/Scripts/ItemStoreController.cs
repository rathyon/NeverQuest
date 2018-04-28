using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace CompleteProject{
	public class ItemStoreController : MonoBehaviour {

		public GameObject item;

		private Text name;
		private Text cost;
		private SpriteRenderer image;


		// Use this for initialization
		void Start () {
			name = item.GetComponent<Trap> ().trapName;
			cost = item.GetComponent<Trap> ().cost;
			image = item.GetComponent<SpriteRenderer> ();


			gameObject.transform.FindChild ("Item Name").gameObject.GetComponent<Text> () = name; 
			gameObject.transform.FindChild ("Item Cost").gameObject.GetComponent<Text> () = cost; 
			gameObject.transform.FindChild ("Item Image").gameObject.GetComponent<SpriteRenderer> ().sprite = image; 
		}

	}
}


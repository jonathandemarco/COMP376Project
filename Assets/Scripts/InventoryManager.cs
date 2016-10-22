using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	public  GameObject[] inventory = new GameObject[2];
	public int holdingWep = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}
		
	void OnTriggerEnter(Collider col){
		if (col.tag == "Item") {
			col.transform.parent = transform;
			col.gameObject.SetActive (false);
		}
	}







}

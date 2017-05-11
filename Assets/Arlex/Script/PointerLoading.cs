using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PointerLoading : MonoBehaviour {

	private float timer = 0f;
	private GameObject obj;
	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("EventSystem");
	}

	public void loading(){
		timer = Time.deltaTime;
		obj.GetComponent<OVRInputModule> ().isgazing = true;
		StartCoroutine ("Drawloading");
	}

	public void resetloading(){
		timer = timer - 1;
		gameObject.GetComponent<Slider> ().value = 0;
	}

	IEnumerator Drawloading(){
		for (float t = timer; t < timer + 1; t += Time.deltaTime) {
			gameObject.GetComponent<Slider> ().value = Mathf.Lerp (-2, 102, t-timer);
			yield return 0;
		}
		obj.GetComponent<OVRInputModule> ().gazed = true;
	}
}

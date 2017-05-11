using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movedoor : MonoBehaviour {

	bool movetoleft = false;
	bool movetoright = true;

	bool lastleft = false;
	bool lastright = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		movetoleft = GameObject.Find ("BodyView").GetComponent<BodySourceView> ().isLeftWave ();
//		movetoright = GameObject.Find ("BodyView").GetComponent<BodySourceView> ().isRightWave ();
//		movetoleft = GameObject.Find ("DetectGesture").GetComponent<GestureController> ().IsPointingDoor();
//		movetoright = GameObject.Find ("DetectGesture").GetComponent<GestureController> ().IsWaingRightHand();

		if (movetoleft && lastleft == false) {
			iTween.MoveBy(gameObject, iTween.Hash("y", -1, "easeType", "easeInOutExpo", "none", "pingPong", "delay", .3));
			lastleft = true;
			lastright = false;
			StartCoroutine (ShowMessage ());
		} else if (movetoright && lastright == false) {
			iTween.MoveBy(gameObject, iTween.Hash("y", 1, "easeType", "easeInOutExpo", "none", "pingPong", "delay", .3));
			lastleft = false;
			lastright = true;
			StartCoroutine (HideMessage ());
		}
	}


	IEnumerator ShowMessage(){
		yield return new WaitForSeconds (1f);
		GameObject.Find ("ShowMessage").layer = LayerMask.NameToLayer ("Default");
	}

	IEnumerator HideMessage(){
		yield return new WaitForSeconds (0);
	    GameObject.Find ("ShowMessage").layer = LayerMask.NameToLayer ("UI");
	}
}

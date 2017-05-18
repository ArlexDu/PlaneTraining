using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginCanvasController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject btnobj = GameObject.Find ("submit");
		Button btn = btnobj.GetComponent<Button> ();
		btn.onClick.AddListener (delegate {
			this.OnClick ();
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnClick(){
		string number = GameObject.Find ("InputField").GetComponent<InputField> ().text;
		Debug.Log ("number is "+number);
		GameObject.Find ("NetWork").GetComponent<NetWork> ().checkUser (number);
	}
}

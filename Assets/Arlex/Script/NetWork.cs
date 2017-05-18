using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NetWork : MonoBehaviour {

	private string baseurl = "182.254.243.44:90/manage";
	private string number;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkUser(string id){
		WWWForm form = new WWWForm ();
		form.AddField ("number", id);
		string url = baseurl + "/checkuser";
		StartCoroutine(sendData(form,url,"Canvas","Login"));
		number = id;
	}

	public void UpLoadLesosn1(string level1,string level2,string level3,string level4,string level5){
		WWWForm form = new WWWForm ();
		form.AddField("level1",level1);
		form.AddField("level2",level2);
		form.AddField("level3",level3);
		form.AddField("level4",level4);
		form.AddField("level5",level5);
		form.AddField ("number", number);
		string url = baseurl + "/setdata/lesson1";
		StartCoroutine(sendData(form,url,"Conclusion","LessonOne"));
	}

	IEnumerator sendData(WWWForm form,string url,string parent,string scene){
		WWW www = new WWW (url, form);
		yield return www;
		if(www.text == "1"){
			switch (scene) {
			case "Login":
				Debug.Log ("number is "+number);
					GameObject network = GameObject.Find ("NetWork"); 
					DontDestroyOnLoad (network);
					SceneManager.LoadScene ("Welcome");
					break;
				case "LessonOne":
					SceneManager.LoadScene ("Menu");
					break;
			}
		}else{
		//	Debug.Log (www.text);
			GameObject p = GameObject.Find (parent);
			FindObject(p,"webinfo").SetActive(true);
		}
	}

	public static GameObject FindObject(GameObject parent,string name){
		Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
		foreach(Transform t in trs){
			if(t.name == name){
				return t.gameObject;
			}
		}
		return null;
	}
		
}

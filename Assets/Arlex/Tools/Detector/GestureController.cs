using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GestureController : MonoBehaviour {

	private KinectGestureEvent point_door = new KinectGestureEvent("point_right",0);
	private KinectGestureEvent helmet = new KinectGestureEvent("helmet",0.5f);   
	private GestureSourceManager manager;
	public GameObject bodymanager;
	private static GestureController instance = null;
	public static GestureController Instance{
		get{
			return instance;
		}
	}

	//initial manager
	public void Start(){
		manager = GestureSourceManager.Instance;
		manager.RemoveDetectors();
		manager.AddDetector ("recognize");
		manager.OnGesture += recognize;
		instance = this;
	}
		

	//judge helmet
	private void recognize (object sender,KinectGestureEvent e){
		if (e.name.Equals ("recognize")) {
			//GameObject.Find ("CreateDiagram").GetComponent<HistogramTexture> ().setHeight (e.confidence);
			if (e.confidence > 0.7) {
			//	PlayerPrefs.SetString("BodyId",e.getOpt("BodyId"));
				//judge event done
				ulong id = Convert.ToUInt64(e.getOpt ("BodyId"));
				Debug.Log ("detect bodyid is "+id);
				manager.confirmBody (id);
				manager.OnGesture -= recognize; 
				DontDestroyOnLoad (bodymanager);
				SceneManager.LoadScene ("Menu");
				manager.RemoveDetectors();
				/*manager.AddDetector ("walk");
				manager.OnGesture += walking;
				Debug.Log ("Add Walk");*/
			}
		}
	}
	//judge walking
	private void walking (object sender,KinectGestureEvent e){
		if (e.name.Equals ("walk")) {
			if (e.confidence > 0.3) {
				//	PlayerPrefs.SetString("BodyId",e.getOpt("BodyId"));
				//judge event done
				manager.OnGesture -= walking; 
				manager.RemoveDetectors();
				GameObject.Find ("Player").GetComponent<PlayerController>().StartTraning();
			}
		}
	}
	//judge point door
	private void pointingDoor (object sender,KinectGestureEvent e){
		if (e.name.Equals ("point_right")) {
			GameObject.Find ("CreateDiagram").GetComponent<HistogramTexture> ().setHeight (e.confidence);
		}
	}
	//judge helmet
	private void Helmet (object sender,KinectGestureEvent e){
		if (e.name.Contains ("helmet")) {
			GameObject.Find ("CreateDiagram").GetComponent<HistogramTexture> ().setHeight (e.confidence);
		}
	}
		
	// load scene form menu to Lesson One	
	public void LoadLessonOne(){
		SceneManager.LoadScene ("LessonOne");
		manager.AddDetector ("walk");
		manager.OnGesture += walking;
	}
		
	// load pointer door gesture
	public void LoadPointer(){
		manager.RemoveDetectors();
		manager.AddDetector ("point_right");
		manager.OnGesture += pointingDoor; 
	}

	// add helmetevent
	public void LoadHelmetP1(){
		manager.RemoveDetectors();
		manager.AddDetector ("helmet_p1");
		manager.OnGesture += Helmet;
	}
	public void LoadHelmetP2(){
		manager.RemoveDetectors();
		manager.AddDetector ("helmet_p2");
		manager.OnGesture += Helmet;
	}
	public void LoadHelmetP3(){
		manager.RemoveDetectors();
		manager.AddDetector ("helmet_p3");
		manager.OnGesture += Helmet;
	}
	public void LoadHelmetP4(){
		manager.RemoveDetectors();
		manager.AddDetector ("helmet_p4");
		manager.OnGesture += Helmet;
	}

	void Update(){

    //manual test
	/*	if (Input.GetKeyDown (KeyCode.A)) {
			manager.RemoveDetectors ();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			manager.OnGesture += walking;
		}*/
	}
}

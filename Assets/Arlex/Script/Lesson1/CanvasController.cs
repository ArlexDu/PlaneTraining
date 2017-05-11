using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasController : MonoBehaviour {

	private GestureController controller;
	private GameObject btnretry;
	private GameObject btncontinue;
	private GameObject judgeData;
	private GameObject conclusion;
	private GameObject videoShow;
	public MovieTexture[] movies;
	public AudioClip[] clips;
	private bool showmovie = true;
	// Use this for initialization
	private float timer = 0f;
	// there are 2 level in this scenario
	private int level = 1;
	void Start () {
		controller = GestureController.Instance;
		ReSetCanvas ();
		List<string> btnsName = new List<string> ();
		btnsName.Add ("Retry");
		btnsName.Add ("Continue");
		btnsName.Add ("Retrain");
		btnsName.Add ("Back");
		btnsName.Add ("Replay");
		btnsName.Add ("Cancel");
		foreach (string btnName in btnsName) {
			GameObject btnobj = GameObject.Find (btnName);
			Button btn = btnobj.GetComponent<Button> ();
			btn.onClick.AddListener (delegate {
				this.OnClick (btnobj);
			});
		}
		btnretry = GameObject.Find ("Retry");
		btncontinue = GameObject.Find ("Continue");
		judgeData = GameObject.Find ("JudgeData");
		conclusion = GameObject.Find ("Conclusion");
		videoShow = GameObject.Find ("Video");
		conclusion.SetActive (false);
		judgeData.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		//manual test

	/*	if (Input.GetKeyDown (KeyCode.A)) {
			CanvasShow ();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			CanvasHide ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			level = 0;
			CanvasHide ();
		}*/
	}


	public void CanvasShow(){
		if (showmovie) {// show video UI
			conclusion.SetActive(false);
			judgeData.SetActive (false);
			videoShow.SetActive (true);
			switch (level) {
			case 1:
				ReSetCanvas ();
				GameObject.Find ("Title").GetComponent<Text> ().text = "应急门指示教学";
				GameObject.Find ("VideoShow").GetComponent<RawImage> ().texture = movies [0];
				GameObject.Find ("VideoShow").GetComponent<AudioSource> ().clip = clips [0];
				break;
			case 2:
				ReSetCanvas ();
				GameObject.Find ("Title").GetComponent<Text> ().text = "氧气面罩使用教学";
				GameObject.Find("VideoShow").GetComponent<RawImage>().texture = movies[1];
				GameObject.Find ("VideoShow").GetComponent<AudioSource> ().clip = clips [1];
				break;
			}
		} else {//show evaluate UI
			conclusion.SetActive(false);
			videoShow.SetActive (false);
			judgeData.SetActive (true);
			switch (level) {
			case 1:
				GameObject.Find ("Title").GetComponent<Text> ().text = "展示应急通道";
				controller.LoadPointer();	
				break;
			case 2:
				GameObject.Find ("Title").GetComponent<Text> ().text = "氧气面罩的使用方法P1";
				controller.LoadHelmetP1();
				break;
			}
			HideButton ();
		}
		timer = Time.deltaTime;
		StartCoroutine ("Show");
	}

	public void CanvasHide(){
		timer = Time.deltaTime;
		StartCoroutine ("Hide");
	}

	public void VideoToEvaluate(){
		timer = Time.deltaTime;
		StartCoroutine ("HideVideo");
	}

	IEnumerator Show(){
		for (float t = timer; t < timer + 1; t += Time.deltaTime) {
			float y =  Mathf.Lerp (25, 10, t-timer);
			transform.position = new Vector3 (transform.position.x,y,transform.position.z);
			yield return 0;
		}

		if (showmovie) {
			MovieTexture ms = GameObject.Find ("VideoShow").GetComponent<RawImage> ().mainTexture as MovieTexture;
			ms.loop = false;
			ms.Play ();
			GameObject.Find ("VideoShow").GetComponent<AudioSource> ().Play ();	
		}
	}

	IEnumerator Hide(){
		for (float t = timer; t < timer + 1; t += Time.deltaTime) {
			float y =  Mathf.Lerp (10, 25, t-timer);
			transform.position = new Vector3 (transform.position.x,y,transform.position.z);
			yield return 0;
		}

	//	Debug.Log ("level is "+level);
		if (level == 1) {//test -1 product 1
			GameObject.Find ("Score").GetComponent<Text>().text = "Socre";
			HideButton ();
			iTween.Resume ();
		} else if(level == 5) {//test 1 product 5
			iTween.Stop ();
			GameObject.Find ("Score").GetComponent<Text>().text = "Socre";
			HideButton ();
			judgeData.SetActive (false);
			PrepareConclusion ();
			StartCoroutine ("Show");
		} else if(level == 0) {//back to original
			showmovie = true;
			GameObject.Find ("Player").GetComponent<PlayerController>().StartTraning();
		}
		level++;
	}

	IEnumerator HideVideo(){
		for (float t = timer; t < timer + 1; t += Time.deltaTime) {
			float y =  Mathf.Lerp (10, 25, t-timer);
			transform.position = new Vector3 (transform.position.x,y,transform.position.z);
			yield return 0;
		}
		CanvasShow ();
	}

	private void OnClick(GameObject sender){
		switch(sender.name){
		case "Continue":
			if (level == 1) {
				showmovie = true;
				CanvasHide ();
			} else if (level == 2) {
				GameObject.Find ("Title").GetComponent<Text> ().text = "氧气面罩的使用方法P2";
				controller.LoadHelmetP2 ();
				GameObject.Find ("Score").GetComponent<Text>().text = "Socre";
				HideButton ();
				level++;
			} else if (level == 3) {
				GameObject.Find ("Title").GetComponent<Text> ().text = "氧气面罩的使用方法P3";
				controller.LoadHelmetP3 ();
				GameObject.Find ("Score").GetComponent<Text>().text = "Socre";
				HideButton ();
				level++;
			} else if (level == 4) {
				GameObject.Find ("Title").GetComponent<Text> ().text = "氧气面罩的使用方法P4";
				controller.LoadHelmetP4 ();
				GameObject.Find ("Score").GetComponent<Text>().text = "Socre";
				HideButton ();
				level++;
			} else if (level == 5) {// go to final pages
				CanvasHide();
			}
			GameObject.Find ("CreateDiagram").GetComponent<HistogramTexture> ().reJudge ();
			break;
		case "Retry":
			GameObject.Find ("CreateDiagram").GetComponent<HistogramTexture> ().reJudge ();
			GameObject.Find ("Score").GetComponent<Text> ().text = "Socre";
			HideButton ();
			break;
		case "Retrain":
			level=0;
			CanvasHide ();
			break;
		case "Back":
			SceneManager.LoadScene ("Menu");
			break;
		case "Replay":
			MovieTexture mr = GameObject.Find ("VideoShow").GetComponent<RawImage> ().mainTexture as MovieTexture;	
			mr.Stop ();
			GameObject.Find ("VideoShow").GetComponent<AudioSource> ().Stop();
			mr.Play ();
			GameObject.Find ("VideoShow").GetComponent<AudioSource> ().Play ();	
			break;
		case "Cancel":
			MovieTexture mc = GameObject.Find ("VideoShow").GetComponent<RawImage> ().mainTexture as MovieTexture;
			mc.Stop ();
			GameObject.Find ("VideoShow").GetComponent<AudioSource> ().Stop();
			showmovie = false;
			VideoToEvaluate ();
			break;
		default:
			Debug.Log("none");
			break;
		}
	}

	// change Canvas's posiion
	private void ReSetCanvas(){
		switch (level) {
		case 1:
			transform.position = new Vector3 (0,25,-79);
			break;
		case 2:
			transform.position = new Vector3 (0,25,-129);
			break;
		}	
	}

	private void HideButton(){
		btnretry.SetActive (false);
		btncontinue.SetActive (false);
	}

	private void ShowButton(){
		btnretry.SetActive (true);
		btncontinue.SetActive (true);
	}


	private float Score_Door;
	private float Score_Helmet_P1;
	private float Score_Helmet_P2;
	private float Score_Helmet_P3;
	private float Score_Helmet_P4;

	// show maxscore and save those score
	public void JudgeFinish(float score){
		ShowButton ();
		float showScore = score * 100;
		GameObject.Find ("Score").GetComponent<Text>().text = showScore.ToString("0.0");
		switch (level) {
		case 1:
			Score_Door = showScore;
			break;
		case 2:
			Score_Helmet_P1 = showScore;
			break;
		case 3:
			Score_Helmet_P2 = showScore;
			break;
		case 4:
			Score_Helmet_P3 = showScore;
			break;
		case 5:
			Score_Helmet_P4 = showScore;
			break;
		}
	}

	// prepare to show conclusion data
	private void PrepareConclusion(){
		Debug.Log ("show conclusion");
		conclusion.SetActive (true);
		GameObject.Find ("DShowDoor").GetComponent<Text> ().text = Score_Door.ToString("0.0");
		GameObject.Find ("DHelmetP1").GetComponent<Text> ().text = Score_Helmet_P1.ToString("0.0");
		GameObject.Find ("DHelmetP2").GetComponent<Text> ().text = Score_Helmet_P2.ToString("0.0");
		GameObject.Find ("DHelmetP3").GetComponent<Text> ().text = Score_Helmet_P3.ToString("0.0");
		GameObject.Find ("DHelmetP4").GetComponent<Text> ().text = Score_Helmet_P4.ToString("0.0");
	}
		
}

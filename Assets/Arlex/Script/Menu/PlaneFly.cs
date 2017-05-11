using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFly : MonoBehaviour {

	private Hashtable args; 
	public float m_speed = 3;
	public Transform[] paths;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (-2000,-800,450);
		args = new Hashtable ();
		args.Add ("path",paths); //设置路径点
		args.Add ("easeType",iTween.EaseType.linear);//set linner
		args.Add ("speed",m_speed);// set speed
		args.Add ("movetopath",false);//whether move form orignal position to the first way point
		args.Add ("orienttopath",true);//wether the model face to the target
		//args.Add ("looktarget",Vector3.zero);//face to a static psition when moving
		args.Add ("loopType","loop");// loop type
		args.Add ("NamedValueColor","_SpecColor");//
		args.Add ("delay",0f);//delay time
		iTween.MoveTo (gameObject,args);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos(){
		iTween.DrawLine (paths, Color.yellow);
		iTween.DrawPath (paths,Color.red);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cubetest : MonoBehaviour {

	public GameObject _hand;

	private Vector3  moveDir;
	private Vector3 handPrePos;

	private HoleSpawner hole=new HoleSpawner ();
	private bool isStart=false ;
	// Use this for initialization

	void Start () {


		Invoke ("StartGame", 5f);
	}
	
	// Update is called once per frame
	void Update () {

		touchEnemy ();
	
		if (isStart) {
			moveDir = _hand .transform.localPosition - handPrePos;
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + moveDir.x * 0.9f, 
		                                            this.transform.localPosition.y + moveDir.y * 0.9f,
		                                            this.transform.localPosition.z);
			handPrePos = _hand .transform.localPosition;
		}    
	}

	void StartGame()
	{
		isStart = true;
		handPrePos = _hand .transform.localPosition;
	}


	public void  touchEnemy()
	{		
		Ray ray = new Ray (this.transform.position, this.transform.position + Vector3.forward*1000f);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, 1000)) {
			if (hitInfo.transform.name.StartsWith("pig"))
			{
				hitInfo.transform.GetComponent<Enemy_Pig>().touchEnemy();
			}
		}
	}
}

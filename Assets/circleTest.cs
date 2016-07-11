using UnityEngine;
using System.Collections;

public class circleTest : MonoBehaviour {

	public GameObject cube;
	[Tooltip("圆的半径")]
	public float radio;
	[Tooltip("一个圆几等分")]
	public int n;

	private float angle;
	private Vector3 cube0;

	private LineRenderer lr;
	private Vector3[] linePoints;
 
	// Use this for initialization
	void Start () {	
		angle = Mathf.PI*2 / n;
		cube0 = new Vector3 (0, 0, 0);
		createNewHoles (n);

		lr = this.GetComponent<LineRenderer > ();
		linePoints = new Vector3[20];

		creatLine (2, 5);
	}

	//当切割圆为n等分点时
	public void createNewHoles(int n)
	{
		for (int i=0; i<n; i++) {
			GameObject go = (GameObject )Instantiate(cube);
			float angle=this.angle *i;
			go.transform.localPosition =new Vector3
				(radio *Mathf.Sin(angle),radio*Mathf.Cos (angle)*-1,0)+cube0;
		}
	}



	public void creatLine(int from,int end)
	{
		float lrAngle = (end - from) * angle / 20;
		Vector3 firstPoint = linePoints [from];
		for (int i=0; i<20; i++) {
			linePoints[i] = new Vector3
				(radio *Mathf.Sin(lrAngle*i),radio*Mathf.Cos (lrAngle*i)*-1,0)+firstPoint;
			lr.SetVertexCount(20);
			lr.SetPosition (i,linePoints[i]);
		}
	}
}

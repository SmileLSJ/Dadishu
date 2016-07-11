using UnityEngine;
using System.Collections;

//
//将手臂的长度添加到洞口的xDis,yDis上
//
public class HoleSpawner : MonoBehaviour
{
    #region  参数
    public GameObject hole;

    public GameObject pigPrefab;

    //洞口在x方向上的间距
    [Tooltip("洞口在x方向上的间距")]
    public float xDis;

    //洞口在y方向上的间距
    [Tooltip("洞口在y方向上的间距")]
    public float yDis;
    // Use this for initialization

    //洞口的索引
    private int holeCount = 0;
	public  Vector3[] holePosition;
    //获取产生曲线的脚本
    private LineRendererManager lineRendererManager;
    //猪出现的位置
    private Vector3 vec;


	//产生圆的方法
	[HideInInspector]
	public  float angle;
	private Vector3 hole0;

	[Tooltip("圆的半径")]
	public float radio;
	[Tooltip("一个圆几等分")]
	public int n;


	//new
	private Vector3[] linePoints;

    #endregion

    void Start () {
		holePosition = new Vector3[n + 1];
		//产生圆的系数
		angle = Mathf.PI*2 / n;
		//第一个洞的位置
		hole0 = new Vector3 (0, 0, 0);
		createNewHoles (n);


        lineRendererManager = GameObject.Find("LineRendererManager").GetComponent<LineRendererManager>();
        //createHoles();

		//线的点
		linePoints = new Vector3[20];
	}


    //产生洞口的代码
    public void createHoles()
    {

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                GameObject go = (GameObject)Instantiate(hole);
                go.transform.localPosition = new Vector3(xDis * i, yDis * j, 0);
                go.transform.parent = this.transform;

                //生成猪
                holePosition[holeCount] = go.transform.localPosition;
                holeCount++;
            }
        }
    }

	//当切割圆为n等分点时
	public void createNewHoles(int n)
	{
		//产生原点出的洞
		holePosition [0] = hole0;
		GameObject go0 = Instantiate (hole, holePosition [0], Quaternion.identity) as GameObject;
		go0 .transform.parent = this.transform;
		//产生圆上的洞
		for (int i=0; i<n; i++) {
			GameObject go = (GameObject )Instantiate(hole);
			go.transform.parent=this.transform;
			float angle=this.angle *i;
			go.transform.localPosition =new Vector3
				(radio *Mathf.Sin(angle),radio*Mathf.Cos (angle)*-1,0)+hole0;
			holePosition[i+1]=go.transform.localPosition;
		}
	}




    //产生猪的脚本
    public void createPig(int index)
    {
        vec = holePosition[index];
        GameObject go = Instantiate(pigPrefab, new Vector3(vec.x-0.07f, vec .y+0.58f, 0),Quaternion.identity) as GameObject;
        go.GetComponent<SpriteRenderer>().sortingOrder = 4;
        go.GetComponent<Enemy_Pig>()._isHurt = false;
    }



    //调用lineRendererManager中的产生曲线的代码
    public void createLine()
    {
        print("显示轨迹");
        if(lineRendererManager .GetComponent <LineRenderer >().enabled ==false )
        {
            lineRendererManager.GetComponent<LineRenderer>().enabled = true;
        }

        float holeXdis = Mathf.Abs(holePosition[0].x - holePosition[3].x)/20;
        Vector3 firstPosition = holePosition[3];
		lineRendererManager.SendMessage("createNewLineRender");
    }
}

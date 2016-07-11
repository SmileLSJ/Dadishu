using UnityEngine;
using System.Collections;


/*
 * 
 * 
 *此脚本主要作用是绘制引导线：
 *
 *   其中采取的方法是在起始点然后再x和y方向加上等间距的倍数，
 *   然后设置给lineReneder来绘制曲线，如果改变曲线
 *
 *   getYPos（）方法：主要用来计算对应于x坐标的y值。
 *
 *   x轴方向上只是从起始然后加上x的等间距获得，没有具体的算法
 *
 * 
 */


public class LineRendererManager : MonoBehaviour
{

    #region 参数

    //用于绘制轨道线的lineReneder
    private LineRenderer _myLR;
    //轨道线上的点的坐标
    private Vector3 [] linePoint;
    //轨道线上的第一个点的坐标
    private Vector3 firstPointPosition;
    //linerenderer的点数
    private int linePointNum = 20;
    private int lineScale = 1;
    //linerenderer的曲线的半径（因为视圆）
    private float radiu;
    //用于在椭圆方法中计算已经产生了几个点的坐标
    private int pointCount = 0;

    //获取洞口
    public HoleSpawner hole;
    //椭圆的长轴、短轴
	[HideInInspector]
    public float a = 1;
	[HideInInspector]
    public float b = 1;

    public static LineRendererManager _instance;

	//new
	private Vector3[] linePoints;
	private float angle;
	private float radio;


    #region 用于判断左手或者右手，以及动作中是在原点的左边或者右边，由外部导入
    //用于判断是左半圆还是右半圆。默认为右半圆,1代表右半圆
    private int isRightCircle = 1;
    //用于判断左手还是右手,默认1为右手
    private int isRightHand = 1;
    #endregion


    #endregion

    void Awake () {
        linePoint = new Vector3[linePointNum];
        _myLR = this.GetComponent<LineRenderer>();
	}

    void Start()
    {
        _instance = this;
		//线的点
		linePoints = new Vector3[20];
		angle = hole.angle;
		radio = hole.radio-0.3f;
    }


    //用于绘制曲线
    //参数表示，在x方向上的间距
    void CreateLineRenderer(float xDis)
    {
        //将现在位置设置为起始位置
        firstPointPosition = new Vector3(0, 0, 0);
		//firstPointPosition = hole.holePosition[1];

        //radiu = xDis * 19;
		radiu = Vector3.Distance (hole.holePosition [0] , hole.holePosition [1]);

        //如果是第一，二个场景的话以（0，0，0）点为中心点,

        //由于左手或者右手影响半圆位置，因为左手和右手正好相反
        //第二个变量：所以乘以一个数如果isRightHand为1时不变为右手，如果为-1，改变方向为左手
        //第一个变量：由于动作影响半圆的位置
        if(GameManager._instance .sceneCount ==1)
        {
            isRightCircle = -1 * isRightHand;
        }
        else
        {
            isRightCircle = 1*isRightHand;
        }

        _myLR.sortingOrder = 5;
        createPoint(GameManager._instance.sceneCount,xDis);    
    }

	public void creatNewLine(int from,int end,int right)
	{
		if (GameManager._instance.sceneCount != 2) {

			float lrAngle = (end - from) * angle / 20;
			Vector3 firstPoint = linePoints [from];
			for (int i=0; i<20; i++) {
				linePoints [i] = new Vector3
				(right * radio * Mathf.Sin (lrAngle * i), radio * Mathf.Cos (lrAngle * i) * -1, 0);
				_myLR.SetVertexCount (20);
				_myLR.SetPosition (i, linePoints [i]);
			}
		} else {
			
			float lrAngle = (end - from) * angle / 20;
			Vector3 firstPoint = linePoints [from];
			for (int i=0; i<20; i++) {
				linePoints [i] = new Vector3
					(right * radio * Mathf.Sin (lrAngle * i)/2, radio * Mathf.Cos (lrAngle * i) * -1, 0)/2-Vector3.up*radio/2;
				_myLR.SetVertexCount (20);
				_myLR.SetPosition (i, linePoints [i]);
			}
		}
	}
    
	public void createNewLineRender()
	{
		switch (GameManager._instance.sceneCount) {
		case 0:
            creatNewLine(1, (Level.scene1_level > 7 ? (14 - Level.scene1_level) : Level.scene1_level), 1 * Level.rightHand);
			break ;
		case 1:
            creatNewLine(1, (Level.scene2_level > 7 ? (14 - Level.scene2_level) : Level.scene2_level), -1 * Level.rightHand);
			break ;
		case 2:
            creatNewLine(1, 7, 1 * Level.rightHand);
			break ;
		case 3:
            creatNewLine(1, (Level.scene4_level > 7 ? (14 - Level.scene4_level) : Level.scene4_level), 1 * Level.rightHand);
			break ;
		}
	}















    //用于计算出曲线中点的Y值
    //第一个参数为x轴的坐标
    //第三个参数代表第几个场景，场景1的索引是0
    float getYPos(float x, float radiu, int sceneCount)
    {
        switch (sceneCount ){
            case 0:
                //场景1的y值相当于圆的1/4
                a = 1;
                b = 1;
                return getCircle(radiu, x);
           
            case 1:
                a = 1;
                b = 1;
                return getCircle(radiu, x);
                
            case 2:
                a = 1;
                b = 0.7f;
                return getEllipse(radiu, x);
                
            case 3:
                a = 1;
                b = 1f;
                return getEllipse(radiu, x);
                
            default :
                return 0;
        }    
    }



    //产生linerenderer点的方法
    private void createPoint(int scene,float xDis)
    {
        #region 进行坐标旋转,移动
        //利用二维旋转矩阵进行旋转，都定为x为最长边
        //默认为不旋转
        float angle = 0;
        //第一，二个场景为x轴大，y轴小,场景是从0开始
        if(scene ==0||scene ==1)
        {
            angle = 0;
        }
        else if(scene ==2)
        {
            angle = -Mathf.PI / 2;

            xDis *=2;
        }
        else if(scene ==3)
        {
            //进行旋转
            angle = -Mathf.PI / 2;

            xDis *= 2;
        }
        #endregion
        
        //产生坐标点的方法
        CreatePointPos(xDis, angle);

    }


    //产生点的位置
    private void CreatePointPos(float xDis,float angle)
    {
        for (int i = 0; i < linePoint.Length; i++)
        {
            Vector3 vec = firstPointPosition + Vector3.right * i * xDis * isRightCircle
                + Vector3.up * getYPos(i * xDis, radiu, GameManager._instance.sceneCount);

            linePoint[i] = new Vector3(Mathf.Cos(angle) * vec.x + Mathf.Sin(angle) * vec.y,
                               (-1) * Mathf.Sin(angle) * vec.x + Mathf.Cos(angle) * vec.y,
                               vec.z);

            _myLR.SetVertexCount(linePoint.Length*lineScale);
            _myLR.SetPosition(i, linePoint[i]);
        }


        if(GameManager ._instance.sceneCount==2)
        {
            for (int i=0;i<linePoint.Length ;i++)
            {
                linePoint[i].x /= 2;
                linePoint[i].y /= 2;
                linePoint[i].y -= (radiu / 2);

                _myLR.SetVertexCount(linePoint.Length * lineScale);
                _myLR.SetPosition(i, linePoint[i]);
            }
        }
    }



    //绘制1/4的圆的方法
    private float getCircle(float Cir_radiu,float x)
    {

        float y = -Mathf.Sqrt(Cir_radiu * Cir_radiu - x * x/a)*b;
        return y;
    }

    //绘制半圆的方法（第四个动作）
      //第一个参数为半径
      //第二个参数为距离第一个点的横坐标的差值
    private float getEllipse(float Cir_radiu,float x)  
    {
        float Point_x = x + firstPointPosition.x;
        return getCircle(Cir_radiu, Point_x);
    }


    //当打完地鼠后，轨迹消失
    public void HideGuiJ()
    {
        this.GetComponent<LineRenderer>().enabled = false;
        print("隐藏轨迹");
    }
}


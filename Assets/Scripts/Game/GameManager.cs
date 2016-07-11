using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using UnityEngine.UI;


/*
 * 负责场景的切换：
 * 
 *   changeScene()：主要负责场景的切换
 *   UseSceneWay()：使用场景中的方法
 */


public class GameManager : MonoBehaviour
{

    #region  参数  1.产生场景的变量  2.场景切换的变量  3.场景切换标志变量

    #region 变量：产生场景的变量
    //使用场景的父类达到可以实现多态
    private SceneFactory _scene;
    //产生场景的工具
    private createScene creater;
    #endregion

    #region 变量：场景切换的变量
    //场景的索引
    public int sceneCount = 0;
    //场景的单利模式
    public static GameManager _instance;
    //各个场景开始的标志
    private bool startScene;
    #endregion

    #region 变量： 场景切换的标志变量
    //记录在每个场景游戏的运行时间
    private float timer;
    //游戏间的间隔时间
    private float timeInteval;
    #endregion

    #region 变量：是否完成一个场景的游戏
    //没有设置
    //判断是否打完一个场景中的地鼠
    private bool isFinish=false;

    public HoleSpawner _hole;
    #endregion

    #region 变量：场景中所有的猪
    private GameObject[] pigs;
    #endregion


	private Level _levelObj;

    //游戏结束的图标
    public GameObject endText;
    #endregion


    void Awake()
    {
        _instance = this;

        //开始第一个场景
        startScene = true;
        isFinish = false;
        timeInteval = 10f;
    }
	
	void Start () {
        creater = new createScene();
        endText.SetActive(false);

		_levelObj = GameObject .Find ("level").GetComponent <Level > ();
	}
	
    //场景切换首先判断是否小于4个场景，然后判断是否调过一次场景，使用startScene来控制只每个场景只调用一次
    //同时在场景限制内，使用方法changeScene来设置切换到下一个场景
	void Update () {


        //总场景为4个
        if(sceneCount <4)
        {
            //每个场景调用一次
            if(startScene)
            {
                //一开始是场景一   因为产生场景的工具是从1开始，而这边的索引是从0开始，需要+1               
               _scene = creater.creaScene(sceneCount + 1);
               
                //使用场景中的方法
               UseSceneWay();
               startScene = false;//用来负责调用一次场景

            }

            //寻找到场景中的所有猪
            FindPigs();

            changeScene();
        }
        else
        {
            //用于跳转到选择界面
            print("游戏结束");
            endText.SetActive(true);
            Invoke("restartGame", 5f);


            Time.timeScale = 500f;
            //游戏结束隐藏轨迹
            LineRendererManager._instance.HideGuiJ();
            //隐藏游戏物体
            _hole.transform.localPosition = new Vector3(_hole.transform.localPosition.x,
                                            _hole.transform.localPosition.y,
                                            -800);
        }        
	}



    #region changeScene方法：  场景切换:用于设置在完成或者时间到的情况下切换到下一个场景
    void changeScene()
    {
        timer += Time.deltaTime;
        //每个场景的时间间隔为5秒
        //或者当碰到pig后，延迟1秒跳转到下一个场景


        //如果没有完成，当达到时间后销毁所有小猪
        if (isFinish == false)
        {
           if (timer >= timeInteval)
           {
               DestroyAllPigs();
           }
        }
        //如果全部点击到，两秒后销毁所有小猪
        else if(isFinish)
        {
            Invoke("DestroyAllPigs", 2f);
        }
    }
    #endregion

    //调用场景函数中的方法
    void UseSceneWay()
    {
        //将场景中的holeSpawner赋值给scene场景
        Scene s = (Scene)_scene;
        s.FindHoleSpanwer(_hole);

        s.createPig();
        s.createLine();
    }


    #region 寻找到场景中所有的猪,判断猪是否都被点击
    void FindPigs()
    {
        //bool isTouchAll = true;
        pigs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in pigs)
        {
            if(go.GetComponent<Enemy_Pig>()._isHurt==false)
            {
                isFinish = false;
            }
            else
            {
                //当打完地鼠后，执行隐藏Linerederer
                LineRendererManager._instance.HideGuiJ();
            }
        }
    }



    //销毁所有的敌人，并且切换到下一个场景
    void DestroyAllPigs()
    {
        for (int i = 0; i < pigs.Length; i++)
        {
            pigs[i].GetComponent<Enemy_Pig>().destroyPig();
        }
        timer = 0;
        sceneCount++;
        startScene = true;

        isFinish = false;

        print("destroyAllPigs");
    }
    #endregion

    #region 结束游戏，跳转到第一个场景
    void restartGame()
    {
		sceneCount = 0;
        Application.LoadLevel("startMeue");
		Destroy (_levelObj.gameObject);

    }
    #endregion
}
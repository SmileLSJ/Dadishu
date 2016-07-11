using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelManager : MonoBehaviour {
    public Text level1Text;
    public Text level2Text;
    public Text level3Text;

    public Text levelText;

    //获取三个物体的父物体
    private Transform _level1;
    private Transform _level2;
    private Transform _level3;
    
    //左右手设置
    public int rightHand;


    //右手
    private int[] scene1Level = new int[3] { 2, 3, 4 };
    private int[] scene2Level = {12,11,10};
    private int[] scene4Level = {3,5,7};
    //左手
    private int[] scene1_Left_Level = new int[3] { 12, 11, 10 };
    private int[] scene2_Left_Level = new int[3] { 2, 3, 4 };
    private int[] scene4_Left_Level = new int[3] { 11, 9, 7 };

    private int scene1_level;
    private int scene2_level;
    private int scene4_level;

	// Use this for initialization
	void Start () {

        _level1 = level1Text.transform.parent;
        _level2 = level2Text.transform.parent;
        _level3 = level3Text.transform.parent;

        setLevelBool(false, false, false);

        //setSceneLevel();
        

        scene1_level = scene1Level[0];
        scene2_level = scene2Level[0];
        scene4_level = scene4Level[0];
	}

    //等级选择按钮
    public void ClickLevelButton(string  str)
    {
        string[] sceneAndLevel = str.Split('-');

        //用来判断不同难度
        switch (sceneAndLevel[1])
        {
            case "1":
                levelText.text  = level1Text.text;
                produceSceneLevel(sceneAndLevel[0], sceneAndLevel[1]);               
                break;
            case "2":
                levelText.text = level2Text.text;
                produceSceneLevel(sceneAndLevel[0], sceneAndLevel[1]);               
                break;
            case "3":
                levelText.text = level3Text.text;
                produceSceneLevel(sceneAndLevel[0], sceneAndLevel[1]);
                
                break;
        }


        setLevelBool(false , false, false);
    }

	//等级菜单按钮
    public void ClickLevelMenu()
    {
        setLevelBool(true, true, true);
    }


    //用于给不同等级难度赋值
    public void produceSceneLevel(string scene,string level)
    {
        switch(scene)
        {
            case "1":
                if (Level.rightHand == 1)
                {
                    scene1_level = scene1Level[int.Parse(level) - 1];
                    Level.scene1_level = scene1_level;
                }
                else if (Level.rightHand == -1)
                {
                    scene1_level = scene1_Left_Level[int.Parse(level) - 1];
                    Level.scene1_level = scene1_level;
                }
                //print("场景"+scene + "  " + "索引    " + scene1_level);
                break;
            case "2":
                if (Level.rightHand == 1)
                {
                    scene2_level = scene2Level[int.Parse(level) - 1];
                    Level.scene2_level = scene2_level;
                }
                else if (Level.rightHand == -1)
                {
                    scene2_level = scene2_Left_Level[int.Parse(level) - 1];
                    Level.scene2_level = scene2_level;
                }
                //print("场景" + scene + "    " + "索引    " + scene2_level);
                break;
            case "4":
                if (Level.rightHand == 1)
                {
                    scene4_level = scene4Level[int.Parse(level) - 1];
                    Level.scene4_level = scene4_level;
                }
                else if (Level.rightHand == -1)
                {
                    scene4_level = scene4_Left_Level[int.Parse(level) - 1];
                    Level.scene4_level = scene4_level;
                }
                //print("场景" + scene + "    " + "索引    " + scene4_level);
                break;
        }
    }


    void Update()
    {
       // setSceneLevel();
    }

    //设置三个的bool值
    void setLevelBool(bool level1,bool level2,bool level3)
    {
        level1Text.gameObject.SetActive(level1);
        level2Text.gameObject.SetActive(level2);
        level3Text.gameObject.SetActive(level3);

        _level1.gameObject.SetActive(level1);
        _level2.gameObject.SetActive(level2);
        _level3.gameObject.SetActive(level3);
    }

    public void setSceneLevel()
    {
        if (Level.rightHand == 1)
        {
            scene1Level = new int[3] { 2, 3, 4 };
            scene2Level = new int[3] { 12, 11, 10 };
            scene4Level = new int[3] { 3, 5, 7 };
        }
        else if (Level.rightHand == -1)
        {
            scene1Level = new int[3] { 12, 11, 10 };
            scene2Level = new int[3] { 2, 3, 4 };
            scene4Level = new int[3] { 11, 9, 7 };
        }
    }

    

   

    /*
    #region 设置三种不同难度的点击事件
    public void ClickLevel1()
    {
        levelText.text = level1Text.text;
        setLevelBool(false, false, false);
       
    }
    public void ClickLevel2()
    {
        levelText.text = level2Text.text;
        setLevelBool(false, false, false);
    }
    public void ClickLevel3()
    {
        levelText.text = level3Text.text;
        setLevelBool(false, false, false);
    }

    #endregion

     */   
}

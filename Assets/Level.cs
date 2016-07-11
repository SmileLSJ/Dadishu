using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    public static Level _instance;
    [HideInInspector ]
    public static int scene1_level;
    [HideInInspector]
	public static int scene2_level;
    [HideInInspector]
	public static int scene4_level;
    [HideInInspector]
	public static int rightHand;
    void Start()
    {
        //默认右手，数值为12
        rightHand = -1;
        _instance = this;
        if (rightHand == 1)
        {
            scene1_level = 2;
            scene2_level = 12;
            scene4_level = 3;
        }
        else if(rightHand ==-1)
        {
            scene1_level = 12;
            scene2_level = 2;
            scene4_level = 11;
        }

    }
    void Update()
    {
        print(rightHand);
        DontDestroyOnLoad(this.gameObject);
       // print(scene1_level);
        print("动作一索引："+scene1_level+"-"+"动作二索引："+scene2_level+"-"+"动作四索引："+scene4_level +"-"+"手："+rightHand);
    }

}

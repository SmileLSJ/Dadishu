using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class actionjuduge : MonoBehaviour {

	// Use this for initialization
    public Text leftText;
    public Text rightText;
    public Text handText;

    public Text level1Text;
    public Text level2Text;
    public Text level3Text;

    public Text levelText;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //判断左右手的选择
    public void JudgeHand()
    {
        string h = handText.text;
        string r = rightText.text;
        if (h == r)
        {
            const int index = 1;
        }
        else
        {
            const int index = -1;
        }
    }
    //判断难度等级
    public void JudgeLevel()
    {
        string text = levelText.text;
        string easy = level1Text.text;
        string general = level2Text.text;
        if (text == easy)
        {
            const int index = 4;
        }
        else if (text == general)
        {
            const int index = 5;
        }
        else
        {
            const int index = 6;
        }
       
    }
    public enum scenceIndex { 
        easy = 4,
        general = 5,
        hard =6,
        left =-1,
        right =1,
    }
    
 


}

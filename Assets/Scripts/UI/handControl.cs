using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class handControl : MonoBehaviour {

    public GameObject _leftHand;
    public GameObject _rightHand;

    public  Text leftText;
    public  Text rightText;
    public  Text handText;

    private  levelManager _levelManeger;

	// Use this for initialization
	void Start () {
        setHandBool(false, false);
        _levelManeger = new levelManager();
	}
	
	// Update is called once per frame
	public void ClickHandButton()
    {
        setHandBool(true, true);
    }



    #region 点击按钮
    //点击左手按钮
    public  void ClickLeftButton()
    {
        handText.text = leftText.text;
        setHandBool(false, false);
        Level.rightHand = -1;
    }
    //点击右手按钮
    public void ClickRightButton()
    {
        handText.text = rightText.text;
        setHandBool(false, false);
        Level.rightHand = 1;
    }

    #endregion

    //设置两个按钮的显示
    void setHandBool(bool leftHand,bool rightHand)
    {
        _leftHand.SetActive(leftHand);
        _rightHand.SetActive(rightHand);
    }

    
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class usercontrol : MonoBehaviour {
    public InputField  username;//定义自动输入窗口
    public InputField  passwordname;
    public Text text_dispalyinfo;
	// Use this for initialization

    private string StrUserName;
    private string StrPW;


    private UIcontrol UIController;
    void Start()
    {
        UIController = GameObject.FindObjectOfType<UIcontrol>();
    }
   

    //判断用户名和密码是否正确
    private bool CheckLogonInfo(string StrUserName, string StrPW) {
        bool returnFlag = false;
        if (StrUserName != null && StrPW != null) {
            if (StrUserName.Trim().Equals("张三") && StrPW.Trim().Equals("123456"))
            {
                returnFlag = true;
            }
        }
        return returnFlag;
    }


    void load() {
        UIController.EnterStartUI();
        
    }
	// Update is called once per frame



    public void judgeUNandPaword()
    {
        StrUserName = username.text;
        StrPW = passwordname.text;    

        print(StrUserName);
        print(StrPW);

        if (CheckLogonInfo(StrUserName, StrPW))
        {
            text_dispalyinfo.text = "登录成功！";
            Invoke("load", 1f);
        }
        else
        {
            text_dispalyinfo.text = "用户名或密码输入错误，请重新输入";
        }
    }

    public void ClearUserUI() {

        
        text_dispalyinfo.text = null;
        passwordname.text = "";
        username.text = "";
    }
	
}

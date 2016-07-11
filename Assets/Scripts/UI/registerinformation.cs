using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class registerinformation : MonoBehaviour {

    public InputField user_name;

    public InputField  user_sex;

    public InputField  pasword;


    public Text text_reminder;

   private string reguser;
    private string regsex;
   private string regpsw ;


    public  UIcontrol UIcontroller;
	// Use this for initialization
	void Start () {
        //UIcontroller = GameObject.FindObjectOfType<UIcontrol>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Load()
    {
        print("sfh ");
        UIcontroller.RegisterEneterStartUI();
    }

    private bool Checkregister(string   reguser, string   regsex, string  regpsw)
    {
        bool registerFlag = false;
        if (reguser != "" && regpsw != "" && regsex != "")
        {
            registerFlag = true;
        }
        return registerFlag;
    }

    public void JudgeRegisterinput()
    {
        reguser =(string ) user_name.text;
        regsex  =(string ) pasword.text;
        regpsw=(string ) user_sex.text;

       

        if (Checkregister (reguser, regsex,regpsw))
        {
            text_reminder.text = "注册成功！";
            Invoke("Load", 1f);
        }
        else
        {
            text_reminder.text = "请输入用户名、性别和密码！";
        }
       

    }


}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIcontrol : MonoBehaviour {
    public GameObject userUI;
    public GameObject startUI;
    public GameObject registerUI;

	// Use this for initialization
    public void ReturnUserUI()
    {
        userUI.SetActive(true);
        startUI.SetActive(false);
    }

    public void EnterStartUI()
    {
        userUI.SetActive(false);
        startUI.SetActive(true);
    }
    public void EnterRegisterUI()
    {
        registerUI.SetActive(true);
        userUI.SetActive(false);
    }

    public void QuickRegisterUI()
    {
        registerUI.SetActive(false);
        userUI.SetActive(true);
    }

    public void RegisterEneterStartUI()
    {
        registerUI.SetActive(false);
        startUI.SetActive(true);
    }

    public void StartGame()
    {
        Application.LoadLevel("dalaoshu");
    }

}

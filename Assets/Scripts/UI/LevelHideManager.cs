using UnityEngine;
using System.Collections;

public class LevelHideManager : MonoBehaviour {

    public GameObject[] levels;

    public void OnMouseExit()
    {
        foreach(GameObject obj in levels )
        {
            if(obj.activeSelf ==true )
            {
                obj.SetActive(false);
            }
        }
    }
}

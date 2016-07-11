using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class levelButton : MonoBehaviour {

    public Text levelText;

    void OnMouseDown()
    {
        levelText.text = this.GetComponent<Text>().text;
    }
}

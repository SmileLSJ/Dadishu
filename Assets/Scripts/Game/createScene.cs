using UnityEngine;
using System.Collections;

public class createScene : MonoBehaviour {

    private SceneFactory _scene;

    private HoleSpawner _hole;

    void Start()
    {
        _hole = GameObject.Find("HoleSpawner").GetComponent<HoleSpawner>();
    }

    //产生场景的方法，参数代表第几个康复动作索引
    public SceneFactory creaScene(int index)
    {
        switch(index)
        {
            case 1:
                print("场景1：侧提手臂与肩同高");
                _scene = new Scene(Level  .scene1_level);
                break;
            case 2:
                print("场景2：右手摸左肩");
                _scene = new Scene(Level .scene2_level );
                break;
            case 3:
                print("场景3：右手摸耳朵");
                _scene = new Scene(0);
                break;
            case 4:
                print("场景4：右手举过头顶");
                _scene = new Scene(Level  .scene4_level );
                break;
        }
        return _scene;
    }
}

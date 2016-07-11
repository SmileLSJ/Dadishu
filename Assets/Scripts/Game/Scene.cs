using UnityEngine;
using System.Collections;

public class Scene : SceneFactory{

    private int _pigIndex;
    private HoleSpawner holespawner;

    public Scene(int pigIndex)
    {
        _pigIndex = pigIndex;
    }

    //找到HoleSpawner脚本
    public void FindHoleSpanwer(HoleSpawner hole)
    {
        holespawner = hole;
    }


    public override void createLine()
    {
        Debug.Log("使用场景中的方法createLine方法");
        holespawner.createLine();
    }

    public override void createPig()
    {
        holespawner.createPig(_pigIndex);
    }
}

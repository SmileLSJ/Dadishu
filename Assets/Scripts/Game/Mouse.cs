using UnityEngine;
using System.Collections;


/*
 * 鼠标脚本：
 * 
 * OnGUI()：用于将鼠标的样式换为指定高度的图片
 * 
 * 
 */


public class Mouse : MonoBehaviour
{

    #region 参数：1.鼠标替代图片   2.鼠标图片的宽和高 3.目标点的位置
    //鼠标图片
    //public Texture mou_Texture; 
	public GameObject target;
    //鼠标图片的宽和高
    private  float mou_PicWidth=70;
    private  float mou_PicHeight=70;

    //目标点的位置，后续可能改为手的屏幕位置，供其他脚本调用
    public Vector3 targetPos;

	private HoleSpawner holes=new HoleSpawner();
    #endregion

    #region 变量：右手控制鼠标；后续需要经过左右手的判断，而后标定控制手的左右；
    public GameObject _HandCtrl;

    #endregion
    void Awake()
    {
        //UnityEngine.Cursor.visible = false;

		targetPos = target.transform.localPosition;
    }

    void Update()
    {
        //UnityEngine.Cursor.visible = false;
		touchEnemy();
    }


    #region 绘制鼠标图样  后面调整为sprite，下面的代码为老版GUI
    // Update is called once per frame
    /*void OnGUI()
    {
        //将鼠标的位置设置为屏幕中的目标点的位置，即可将图片显示到指定点的位置上
        targetPos.x = _HandCtrl.transform.position.x*100f;
        targetPos.y = _HandCtrl.transform.position.y*100f;

        Vector2 target_pos = targetPos;

		target_pos = holes.holePosition [3];

        GUI.DrawTexture(new Rect(target_pos.x - mou_PicWidth / 2, Screen.height - target_pos.y - mou_PicHeight / 2,
            mou_PicWidth, mou_PicHeight), mou_Texture);
        touchEnemy();
    }
    */
    #endregion


    #region  碰到物体后改变鼠标的样式
    void changeMouStyle(int tex_width,int tex_height)
    {
        mou_PicWidth = tex_width;
        mou_PicHeight = tex_height;
    }
    #endregion


    #region 通过射线检测，判断是否碰到pig，如果碰到调用Enemy_Pig的方法touchEnemy()
    public void  touchEnemy()
    {
		Ray ray = Camera.main.ScreenPointToRay(target.transform.localPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10000f))
        {
			print (121212);
            print(hitInfo.transform.name);
            if (hitInfo.transform.name.StartsWith("pig"))
            {
                hitInfo.transform.GetComponent<Enemy_Pig>().touchEnemy();
                //changeMouStyle(120,120);
            }
        }
        else
        {
            //changeMouStyle(70, 70);
        }

        #region  2D碰撞失败的脚本
        //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.transform.position, Input.mousePosition , layerMask);
        //if (hitInfo.transform.gameObject != null)
        //{
        //    if(hitInfo.transform.name.StartsWith("pig"))
        //    {
        //        hitInfo.transform.GetComponent<Enemy_Pig>().touchEnemy();
        //    }
        //}
        #endregion
    }
    #endregion


}

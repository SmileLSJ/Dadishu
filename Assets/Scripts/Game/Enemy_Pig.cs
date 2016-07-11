using UnityEngine;
using System.Collections;

/*
 * 敌人（猪）的脚本 
 *
 * 1.touchEnemy():用来设置碰到Enemy，将isHurt设置为true
 * 2.closeEnemyHurt():用来设置关闭enemhurt动画，执行的场所在animation中的动画编辑器中。
 * 
 */


public class Enemy_Pig : MonoBehaviour
{
    #region 参数：1.状态机  2.是否碰到敌人，默认为false，表示没有碰到
    private Animator _animator;

    //用来判断是否碰到敌人
    [HideInInspector]
    public bool _isHurt = false;

    //public static Enemy_Pig  _instance;
    #endregion


    void Start()
    {
        _animator = this.GetComponent<Animator>();
        //_instance = this;
    }

    void Update()
    {
        //测试代码
        //test();

        #region 猪状态的转换
        //如果碰到敌人
        if (_isHurt)
        {
            _animator.SetBool("isHurt", true);//isHurt(animator中的变量)是用来判断是否碰到敌人
            //如果碰到敌人，2秒后销毁
            Destroy(this.gameObject, 2f);
        }
        else   //经过多少后转为一直受伤的状态
        {
            _animator.SetBool("isHurt", false);
        }
        #endregion
    }


    #region 用来设置碰到Enemy，将isHurt设置为true
    public void touchEnemy()
    {
        _isHurt = true;
    }
    #endregion


    #region 用来设置关闭enemyhurt的动画
    private void closeEnemyHurt()
    {
        if (_isHurt)
            _isHurt = false;
    }
    #endregion

    //用于销毁当前对象
    public void destroyPig()
    {
        Destroy(this.gameObject);
    }







    #region 测试代码
    public void test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            touchEnemy();
        }
    }
    #endregion

}

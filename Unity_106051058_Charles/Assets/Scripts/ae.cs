using UnityEngine;


public class ae : MonoBehaviour
{
    #region 欄位區域
    [Header("移動速度")]
    [Range(1, 3000)]
    public int speed = 10; 
    [Header("旋轉速度"), Tooltip("旋轉速度"), Range(1.5f, 200f)]
    public float turn = 20.5f;       
    [Header("是否完成任務")]
    public bool mission;              
    [Header("玩家名稱")]
    public string _name = "ae"; 
    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    #endregion

    [Header("檢物品位置")]
    public Rigidbody rigCatch;

    private void Update()
    {
        Turn();
        Run();
        Catch();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Steak" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }

        if (other.name == "area" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            GameObject.Find("Steak").GetComponent<HingeJoint>().connectedBody = null;
        }
    }

    #region 方法區域


    private void Run()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack")) return;

        float v = Input.GetAxis("Vertical");
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);

        ani.SetBool("walk", v != 0);
    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("attack");
        }
    }
    #endregion
}
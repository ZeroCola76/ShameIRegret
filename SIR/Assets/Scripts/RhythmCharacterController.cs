using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmCharacterController : MonoBehaviour, IObserver
{
    public GameObject zero;
    public GameObject velvet;
    //private Animator animator;
    public Image Gameover;
    private bool isDead; //급하게 추가
    private NodeOutPuts nodeOutPuts;
    // Start is called before the first frame update
    void Start()
    {
        nodeOutPuts = this.GetComponent<NodeOutPuts>();
       

        EventManager.Instance.RegisterObserver(EventType.rhythmHurt, this);
        EventManager.Instance.RegisterObserver(EventType.rhythmDie, this);
    }

    public void OnNotify(EventType eventType)
    {
        switch (eventType)
        {
            case EventType.rhythmHurt:
                Debug.Log("리듬겜하다가 맞았다!");
                //animator.SetTrigger("hurt");
                break;
            case EventType.rhythmDie:
                Debug.Log("리듬겜하다가 죽어버렸다!");
                //animator.SetTrigger("die");
                isDead = true;
                Gameover.gameObject.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        ///이건 좋지 않다.빌드땜에 이렇게 하는거지 후에 무조건 리팩토링
        if (GameManager.Instance.isVelvet)
        {
            zero.SetActive(false);
            velvet.SetActive(false);
            //animator = velvet.GetComponent<Animator>();
        }
        else
        {
            zero.SetActive(false);
            velvet.SetActive(false);
            //animator = zero.GetComponent<Animator>();
        }
        ///ㅇㅇ

        if ((isDead) && Input.GetKeyDown(KeyCode.Escape))
        {
            isDead = false;
            nodeOutPuts.combo.text = "0";
            Gameover.gameObject.SetActive(false);
            PoolManager.Instance.InitializePools();
            //animator = null;
            CumstomSceneManager.Instance.LoadScene(3);
        }
    }

    //private void ResetTrigger(string triggerName)
    //{
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    if (stateInfo.IsName(triggerName))
    //    {
    //        animator.ResetTrigger(triggerName);
    //        animator.SetTrigger(triggerName);
    //    }
    //    else
    //    {
    //        animator.SetTrigger(triggerName);
    //    }
    //}

}

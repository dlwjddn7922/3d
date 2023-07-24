using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval : MonoBehaviour
{
    [SerializeField] Transform eval;
    [SerializeField] Animator animator;

    bool isMove = false;
    int curFloor = 0;
    int moveFloor = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            if(curFloor > moveFloor)
            {
                if (curFloor <= eval.transform.position.y + 0.5f)
                {
                    moveFloor = curFloor;
                    isMove = false;
                }
                eval.transform.Translate(Vector3.up * Time.deltaTime * 1f);
            }
            else
            {
                if (curFloor <= eval.transform.position.y + 0.5f)
                {
                    moveFloor = curFloor;
                    isMove = false;
                }
                eval.transform.Translate(Vector3.down * Time.deltaTime * 1f);
            }
            
        }
    }
    public void OnSetPos(int floor)
    {
        if (isMove)
            return;
        isMove = true;
        curFloor = floor;
    }

    public void OnOpen()
    {
        //animator.SetTrigger("Open");
        StartCoroutine("COpen");
    }

    public void OnClose()
    {
        //animator.SetTrigger("Close");
        StartCoroutine("CClose");
    }

    IEnumerator COpen()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("OpenEm");
    }
    IEnumerator CClose()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Empty");
    }
}

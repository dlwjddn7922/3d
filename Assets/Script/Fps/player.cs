using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform parent;
    [SerializeField] Transform bulletParent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject itemBox;
    public float fireDelayTime;
    private float fireTimer;
    bool isAiming = false;
    bool isRun = false;
    //public bool isPick = false;
    [HideInInspector] public List<itemBox> item = new List<itemBox>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(isAiming)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", z);
        }
        else
        {
            /*if(x == 0 && z == 0)
            {
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                if(isRun)
                {
                    animator.SetFloat("Speed", 1f);
                }
                else
                {
                    animator.SetFloat("Speed",0.3f);
                }
            }*/
            transform.Translate(new Vector3(x, 0f, z) * Time.deltaTime * 3f);

        }
        //animator.SetFloat("X", x);
        //animator.SetFloat("Y", z);
        if (x == 0 && z == 0)
        {
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.3f);
        }

        //���콺 Ŭ������ �߻� �Ѵ�.
        fireTimer += Time.deltaTime;
        if(fireTimer >= fireDelayTime)
        {
            if (Input.GetKey(KeyCode.Mouse0) && isAiming == true)
            {
                Bullet b = Instantiate(bullet, parent);
                b.transform.SetParent(bulletParent);
                fireTimer = 0;
                b.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Aiming",true);
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Aiming", false);
            isAiming = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("Roll");
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetTrigger("Pickup");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reloading");
        }


        //���콺�� ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(mouseRay,out rayDistance))
        {
            Vector3 lookPosition = mouseRay.GetPoint(rayDistance);
            lookPosition.y = transform.position.y; //�÷��̾��� ���̷� y��ǥ�� ����

            //�÷��̾ ���콺 ������ �ٶ󺸵��� ȸ��
            transform.LookAt(lookPosition);
        }
        
    }
    public void FootStep()
    {

    }
    public void EndPickup()
    {
        if (item.Count == 0)
            return;

        Destroy(item[0].gameObject);
        item.RemoveAt(0);
    }
    public void RemoveItem(itemBox items)
    {
        if(item.Contains(items))
        {
            foreach (var tem in item)
            {
                if(items == tem)
                {
                    item.Remove(items);
                    break;
                }
            }
        }
    }
}

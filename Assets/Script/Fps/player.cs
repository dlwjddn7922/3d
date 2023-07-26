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

        //마우스 클릭으로 발사 한다.
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


        //마우스의 스크린 좌표를 월드 좌표로 변환
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(mouseRay,out rayDistance))
        {
            Vector3 lookPosition = mouseRay.GetPoint(rayDistance);
            lookPosition.y = transform.position.y; //플레이어의 높이로 y좌표를 조정

            //플레이어가 마우스 방향을 바라보도록 회전
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

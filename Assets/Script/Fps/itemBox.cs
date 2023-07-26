using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBox : MonoBehaviour
{
    player p;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<player>())
        {
            p = other.GetComponent<player>();
            other.GetComponent<player>().item.Add(this);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (p == null)
            return;
        p.RemoveItem(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Destroy : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }


    void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = target.transform.position;
        }
    }
}
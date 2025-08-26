using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    bool init = false;
    public virtual bool Init()
    {
        if (init)
            return false;

        init = true;
        return true;
    }

    void Update()
    {
        
    }
}

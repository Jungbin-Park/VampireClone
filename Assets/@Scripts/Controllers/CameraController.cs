using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}

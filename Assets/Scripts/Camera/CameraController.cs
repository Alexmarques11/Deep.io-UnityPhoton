using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    // Método para atribuir o target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

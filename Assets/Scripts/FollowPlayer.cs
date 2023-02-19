using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}

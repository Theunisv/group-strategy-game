using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodsFalling : MonoBehaviour
{
    public bool _falling = false;
    public void Fall()
    {
        _falling = true;
    }

    private void Update()
    {
        if (_falling)
        {
            transform.position = new Vector2( transform.position.x, transform.position.y - 1.5f);
            transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z + 0.009f,transform.rotation.w);
        }
    }
}

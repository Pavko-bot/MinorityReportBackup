using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isColliding;
    public bool useLimitation;

    private float oldValue;

    public void setIsColliding(bool isCurrentlyColliding)
    {
        isColliding = isCurrentlyColliding;
    }

    private void Start()
    {
        oldValue = 0f;
        transform.position = new Vector3(oldValue, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (useLimitation)
        {
            var newValue = Mathf.Clamp(transform.position.x, 0, 140);
            if(newValue <= 0f)
            {
                transform.position = new Vector3(0.01f, Mathf.Clamp(transform.position.y, transform.position.y,
                transform.position.y), Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
                oldValue = newValue;
            }
            if (Mathf.Abs(oldValue - newValue) < 8f)
            {
            transform.position = new Vector3(newValue, Mathf.Clamp(transform.position.y, transform.position.y,
                transform.position.y), Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
                oldValue = newValue;
            }
            else
            {
                transform.position = new Vector3(oldValue, transform.position.y, transform.position.z);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed;
    public Vector3 offsetEndPos;
    public bool circularMotion;
    public bool verticalCircle;
    public float timeCounter;
    public float width;
    public float height;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Awake()
    {
        startPos = transform.position;
        targetPos = startPos + offsetEndPos; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (circularMotion)
        {
            timeCounter += Time.deltaTime * speed;
            float x = startPos.x + Mathf.Cos(timeCounter) * width;

            if (verticalCircle)
            {
                float y = startPos.y + Mathf.Sin(timeCounter) * height;
                float z = startPos.z;
                transform.position = new Vector3(x, y, z);
            }
            else
            {
                float y = startPos.y;
                float z = startPos.z + Mathf.Sin(timeCounter) * height;
                transform.position = new Vector3(x, y, z);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (transform.position == targetPos)
            {
                if (targetPos == startPos + offsetEndPos)
                    targetPos = startPos;
                else if (targetPos == startPos)
                    targetPos = startPos + offsetEndPos;
            }
        }
    }
}

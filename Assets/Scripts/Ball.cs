using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float HoriResistance = 5.0f;
    private static float GravitySpeed = 4.0f;
    private static float GravityAcc = 8.0f;

    private bool mStill = true;

    private Vector3 mVelocity;

    public void Hit(Vector3 velocity)
    {
        mVelocity = velocity;
        mStill = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(new Vector3(10, 10, 0));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            mStill = true;
        }

        if (!mStill)
        {
            mVelocity.x = Mathf.MoveTowards(mVelocity.x, 0.0f, HoriResistance * Time.deltaTime);
            mVelocity.y = Mathf.MoveTowards(mVelocity.y, -GravitySpeed, GravityAcc * Time.deltaTime);

            transform.position = transform.position + mVelocity * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.ToString());
    }
}

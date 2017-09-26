using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public GameObject mModel;

    private Vector3 mBasePosition;
    private bool mMouseDown;
    private bool mShot;

    private Vector3 mBaseShoot;
    private float mAngleAcc = 5000.0f;
    private float mAngleSpeed;
    private float mAngle;
    private float mShotTime;

    public AnimationCurve mShotCurve;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mBasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mBasePosition.z = 0.0f;
            mMouseDown = true;
            mShot = false;
            transform.position = mBasePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mMouseDown = false;
            mAngle = 0.0f;
            mAngleSpeed = 0.0f;
            mShotTime = 0.0f;
            mShot = true;
        }

        if (!mMouseDown && !mShot)
            mModel.SetActive(false);

        if (mShot)
        {
            mShotTime += Time.deltaTime * 4.0f;

            //mAngleSpeed += mAngleAcc * Time.deltaTime;
            //mAngle += mAngleSpeed * Time.deltaTime;

            mAngle = mShotCurve.Evaluate(mShotTime) * 180.0f;

            Quaternion q = Quaternion.AngleAxis(mAngle, Vector3.back);

            var rocketUp = q * mBaseShoot;
            transform.up = rocketUp;
        }

        if (mMouseDown)
        {
            var currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0.0f;

            if (Vector3.Distance(mBasePosition, currentPosition) > 0.5f)
            {
                mBaseShoot = (currentPosition - mBasePosition).normalized;
                transform.up = mBaseShoot;
                mModel.SetActive(true);
            }
            else
                mModel.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!mShot)
            return;

        var ball = collider.gameObject.GetComponent<Ball>();
        if (ball == null)
            return;

        float ballDist = Vector3.Distance(ball.transform.position, transform.position);
        float racketLen = transform.localScale.y;

        float distancePower = Mathf.Max(ballDist / 1.5f, 0.5f);
        float anglePower = 1.0f;

        if (mShotTime >= 0.4f && mShotTime <= 0.6f)
        {
            anglePower = 1.0f;
        }
        else if (
            (mShotTime > 0.2f && mShotTime < 0.4f) ||
            (mShotTime < 0.8f && mShotTime > 0.6f))
        {
            anglePower = 0.7f;
        }
        else
        {
            anglePower = 0.4f;
        }

        Debug.LogFormat("distance power = {0}, angle power = {1}, shot time = {2}", distancePower, anglePower, mShotTime);

        float power = 18.0f * distancePower * anglePower;

        ball.Hit(transform.right * power);
    }
}

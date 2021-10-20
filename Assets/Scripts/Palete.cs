using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palete : MonoBehaviour
{
    public const float kBallRadius = 0.15f;
    public const float kRacketHalfWidth = 0.1f;
    private const float kAngleAcceleration = 8000.0f;

    private enum State
    {
        Idle,
        Position,
        Angle,
        Shot,
    }

    public GameObject racket;
    public GameObject racketReference;
    public GameObject racketPivot;
    public Ball ball;
    public bool collision;

    private State state;
    private Vector3 initialUp;
    private float swing;
    private float swingAngleSpeed = 0.0f;
    private float prevShotAngle;

    // Start is called before the first frame update
    void Start()
    {
        // ball.AddVelocity(Vector2.right);
        ball.SetStill(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0.0f;
                    transform.position = position;
                    state = State.Position;

                    racket.SetActive(false);
                    racketReference.SetActive(false);
                    racketPivot.SetActive(true);
                }
                break;

            case State.Position:
                if (Input.GetMouseButtonDown(0))
                {
                    collision = false;
                    swingAngleSpeed = 0.0f;

                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchWorldPosition.z = 0.0f;
                    transform.up = (touchWorldPosition - transform.position).normalized;
                    initialUp = transform.up;
                    state = State.Angle;

                    racket.SetActive(true);
                    racketReference.SetActive(true);
                    racketPivot.SetActive(false);
                }
                break;

            case State.Angle:
                if (Input.GetMouseButton(0))
                {
                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchWorldPosition.z = 0.0f;

                    transform.up = (touchWorldPosition - transform.position).normalized;

                    if ((transform.up - initialUp).sqrMagnitude > Vector3.kEpsilon)
                    {
                        float angleSign = Vector3.SignedAngle(transform.up, initialUp, Vector3.forward);
                        if (Mathf.Abs(angleSign) <= 90.0f)
                        {
                            swing = angleSign;
                        }

                        if (Mathf.Sign(angleSign) != Mathf.Sign(swing))
                        {
                            transform.up = -initialUp;
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    prevShotAngle = Vector3.Angle(transform.up, initialUp);
                    state = State.Shot;
                }
                break;

            case State.Shot:
                swingAngleSpeed += kAngleAcceleration * Time.deltaTime;
                transform.up = Quaternion.AngleAxis(swingAngleSpeed * Time.deltaTime * Mathf.Sign(swing), Vector3.forward) * transform.up;

                float angle = Vector3.Angle(transform.up, initialUp);

                if (!collision &&
                    MathUtils.RayPointDistance(new Ray2D(transform.position, transform.up), ball.transform.position, out float distanceOnRacket) < kBallRadius + kRacketHalfWidth &&
                    distanceOnRacket >= 0.0f &&
                    distanceOnRacket <= 1.0f)
                {
                    float linearSpeed = MathUtils.GetLinearSpeed(swingAngleSpeed * Mathf.Deg2Rad, distanceOnRacket);
                    Debug.Log($"Collision, linearSpeed={linearSpeed}");
                    collision = true;

                    Vector2 racketReflectDirection = GetPaletteSide(ball.transform.position) ? transform.right : -transform.right;
                    ball.SetStill(false);
                    ball.Reflect(racketReflectDirection);
                    ball.AddVelocity(racketReflectDirection * linearSpeed * 1.0f);
                }

                if (Mathf.Abs(angle) < 0.1 || Mathf.Abs(angle) > Mathf.Abs(prevShotAngle))
                {
                    transform.up = initialUp;
                    state = State.Idle;
                }

                prevShotAngle = angle;

                break;
        }

        // Debug.Log($"side={GetPaletteSide(ball.transform.position)}");
    }

    private bool GetPaletteSide(Vector3 point)
    {
        return Mathf.Sign(Vector3.Cross(transform.up, (point - transform.position)).z) < 0.0f;
    }
}

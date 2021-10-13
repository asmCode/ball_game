using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palete : MonoBehaviour
{
    private enum State
    {
        Idle,
        Position,
        Angle,
        Shot,
    }

    public GameObject palete;

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    palete.transform.position = position;
                    state = State.Position;
                }
                break;

            case State.Position:
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchWorldPosition.z = 0.0f;
                    palete.transform.up = (touchWorldPosition - palete.transform.position).normalized;
                    state = State.Angle;
                }
                break;

            case State.Angle:
                if (Input.GetMouseButton(0))
                {
                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchWorldPosition.z = 0.0f;
                    palete.transform.up = (touchWorldPosition - palete.transform.position).normalized;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    state = State.Shot;
                }
                break;

            case State.Shot:

                break;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPointTester : MonoBehaviour
{
    public Transform ball;
    public Palete racket;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"distance={MathUtils.RayPointDistance(new Ray2D(racket.transform.position, racket.transform.up), ball.transform.position)}");
    }
}

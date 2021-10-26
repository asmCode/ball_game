using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Ball ball;

    private Vector2 ballInitialPosition;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        ballInitialPosition = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        ball.SetStill(true);
        ball.Stop();
        ball.transform.position = ballInitialPosition;
    }
}

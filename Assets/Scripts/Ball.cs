using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float kReflectLoss = 0.8f;
    private static Vector2 kGravity = new Vector2(0, -1);

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocity += kGravity * Time.deltaTime;

        Vector2 position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    public void AddVelocity(Vector2 velocity)
    {
        this.velocity += velocity;
    }

    public void Reflect(Vector2 normal)
    {
        velocity = Vector2.Reflect(velocity, normal) * kReflectLoss;
    }
}

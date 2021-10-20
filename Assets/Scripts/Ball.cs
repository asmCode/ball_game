using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float kReflectLoss = 0.8f;
    private static Vector2 kGravity = new Vector2(0, -9.8f);

    private Vector2 velocity;

    bool isStill;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStill)
        {
            return;
        }

        velocity += kGravity * Time.deltaTime;
        velocity -= velocity * velocity.magnitude * 0.09f * Time.deltaTime;

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

    public void SetStill(bool isStill)
    {
        this.isStill = isStill;
    }
}

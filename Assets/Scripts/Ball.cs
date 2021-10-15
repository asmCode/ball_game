using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    public void AddVelocity(Vector2 velocity)
    {
        this.velocity += velocity;
    }
}

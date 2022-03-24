using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    // direction that the ship flies
    [SerializeField] bool flyTopDown = true;

    // speed the ship flies automatically
    [SerializeField] float autoSpeed = 0f;

    // Speed controlled
    [SerializeField] protected float flySpeed = 0f;

    // not serialized members
    private Vector3 autoFlyDirection = Vector3.back;

    // protected members
    protected Vector3 flyDirection = Vector3.zero;

    private void Awake()
    {
        // if it doesn't comes from the top of the screen, it comes from the bottom
        if (!flyTopDown) autoFlyDirection = Vector3.forward;

        // prevent the ships from flyinh in the wrong direction
        if (autoSpeed < 0) autoSpeed = 0;
    }

    private void Update()
    {
        UpdateShip();
        Move();
        LateUpdateShip();
    }

    // Moves the ship
    protected virtual void Move()
    {
        Vector3 finalFlySpeed = autoFlyDirection * autoSpeed + flyDirection * flySpeed;
        transform.position += finalFlySpeed * Time.deltaTime;
    }

    protected abstract void UpdateShip();
    protected abstract void LateUpdateShip();
}

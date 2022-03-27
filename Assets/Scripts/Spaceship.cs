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

    private bool isMovingTo = false;
    private Vector3 moveToPosition;
    private float moveToSpeed = 5f;
    private System.Action<PlayerShip> finishAction;

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
        Vector3 finalFlySpeed;

        if (!isMovingTo)
        {
            finalFlySpeed = autoFlyDirection * autoSpeed + flyDirection * flySpeed;
        }
        else
        {
            Vector3 direction = (moveToPosition - transform.position).normalized;
            finalFlySpeed = direction * moveToSpeed;
        }

        transform.position += finalFlySpeed * Time.deltaTime;

        if (isMovingTo)
        {
            VerifyMoveTo();
        }
    }

    public void MoveTo(Vector3 position, float speed, System.Action<PlayerShip> finishAction)
    {
        moveToPosition = position;
        moveToSpeed = speed;
        isMovingTo = true;
        this.finishAction += finishAction;
    }

    void VerifyMoveTo()
    {
        if (Vector3.Distance(transform.position, moveToPosition) < 0.05f)
        {
            isMovingTo = false;
            finishAction?.Invoke((PlayerShip)this);
        }
    }

    protected abstract void UpdateShip();
    protected abstract void LateUpdateShip();
}

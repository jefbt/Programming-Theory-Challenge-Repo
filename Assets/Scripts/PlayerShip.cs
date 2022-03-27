using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Spaceship
{
    // Prevents the ship from going outside the screen
    [SerializeField] Vector2 movementBoundaries;

    // Angle the ship will have when going left and right
    [SerializeField] float turnAngle = 30f;

    bool canBeControlled = true;

    protected override void UpdateShip()
    {
        if (canBeControlled)
        {
            // Gets the player input movement in the horizontal and vertical axis
            flyDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else
        {
            flyDirection = Vector3.zero;
        }

        Roll(flyDirection.x);
    }

    protected override void LateUpdateShip()
    {
        if (canBeControlled)
        {
            // protect the ship from going outside the screen
            if (transform.position.x < -movementBoundaries.x)
            {
                transform.position = new Vector3(-movementBoundaries.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > movementBoundaries.x)
            {
                transform.position = new Vector3(movementBoundaries.x, transform.position.y, transform.position.z);
            }

            // uses boundaries Y as player position Z, as the ship is seen topdown
            if (transform.position.z < -movementBoundaries.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -movementBoundaries.y);
            }
            else if (transform.position.z > movementBoundaries.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, movementBoundaries.y);
            }
        }   
    }

    // (don') do a barrel roll
    // Rolls the ship in desired angle, to simulate going left and right
    void Roll(float direction)
    {
        transform.rotation = Quaternion.Euler(0, 0, -direction * turnAngle);
    }

    public void RemovePlayerControl()
    {
        SetPlayerControl(false);
    }

    public void GivePlayerControl()
    {
        SetPlayerControl(true);
    }

    public void SetPlayerControl(bool control)
    {
        canBeControlled = control;
    }
}

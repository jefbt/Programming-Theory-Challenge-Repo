using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateControl : MonoBehaviour
{
    [SerializeField] RectTransform playerIcon;
    [SerializeField] RectTransform trackTransform;

    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    [SerializeField] Transform playerFinalPosition;
    [SerializeField] float finalMoveSpeed = 5f;

    [SerializeField] float distanceFromEndToFinish = 5f;

    [SerializeField] float speed = 5f;

    [Tooltip("If true, the gate will decrease it's Z value")]
    [SerializeField] bool isGoDown = true;

    [SerializeField] GameObject playerFinishCollider;

    [SerializeField] GameObject gatePortal;

    public float finalWaitTime { get; private set; } = 0f;

    float trackSize;
    float gateDistance;
    float distanceZ;
    bool isTraveling = true;

    private void Awake()
    {
        trackSize = Mathf.Abs(trackTransform.rect.height);
        gateDistance = Mathf.Abs(endPosition.position.z - startPosition.position.z);
        transform.position = startPosition.position;
        if (isGoDown)
        {
            distanceZ = endPosition.position.z + distanceFromEndToFinish;
        }
        else
        {
            distanceZ = endPosition.position.z - distanceFromEndToFinish;
        }

        speed *= 1f - MainMenuManager.difficulty / 10f;
    }

    private void Update()
    {
        if (isTraveling)
        {
            UpdateSelfPosition();
            UpdateTrackPosition();
        }
    }

    void UpdateSelfPosition()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (isGoDown)
        {
            if (transform.position.z < endPosition.position.z)
            {
                ReachedGate();
            }
            else if (transform.position.z < distanceZ)
            {
                StopSpawning();
            }
        }
        else
        {
            if (transform.position.z > endPosition.position.z)
            {
                ReachedGate();
            }
            else if (transform.position.z > distanceZ)
            {
                StopSpawning();
            }
        }
    }

    void UpdateTrackPosition()
    {
        playerIcon.anchoredPosition = new Vector2(0, GetPercentPosition() * trackSize);
    }

    float GetPercentPosition()
    {
        float pos = Mathf.Abs(transform.position.z - startPosition.position.z);
        return Mathf.Abs(pos / gateDistance);
    }

    void StopSpawning()
    {
        foreach(ObjectSpawner spawner in FindObjectsOfType<ObjectSpawner>())
        {
            spawner.StopSpawning();
        }
    }

    void ReachedGate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, endPosition.position.z);
        isTraveling = false;
        GameManager.ReachGate(this);
    }

    public void SetPlayerFinishPositionActive()
    {
        playerFinishCollider.SetActive(true);
        gatePortal.GetComponent<Animator>().SetTrigger("start");
    }

    public void TurnOffFinishCollider()
    {
        playerFinishCollider.SetActive(false);
    }

    public Vector3 GetPlayerFinalPosition()
    {
        return playerFinalPosition.position;
    }

    public float GetFinalMoveSpeed()
    {
        return finalMoveSpeed;
    }
}

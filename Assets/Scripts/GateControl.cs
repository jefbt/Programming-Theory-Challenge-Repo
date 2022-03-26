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

    [SerializeField] float speed = 5f;

    [Tooltip("If true, the gate will decrease it's Z value")]
    [SerializeField] bool isGoDown = true;

    float trackSize;
    float gateDistance;
    bool isTraveling = true;

    private void Awake()
    {
        trackSize = Mathf.Abs(trackTransform.rect.height);
        gateDistance = Mathf.Abs(endPosition.position.z - startPosition.position.z);
        transform.position = startPosition.position;
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
        }
        else
        {
            if (transform.position.z > endPosition.position.z)
            {
                ReachedGate();
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

    void ReachedGate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, endPosition.position.z);
        isTraveling = false;
        GameManager.ReachGate();
    }
}

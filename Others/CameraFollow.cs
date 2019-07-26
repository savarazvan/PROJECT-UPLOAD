using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static Transform target;
    Transform camPos;
    public Vector2 boundaryMin, boundaryMax;
    float holdposX;
    public float fov;
    public float smoothTime;
    public Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Vector3 smoothedPosition;
    Vector3 nextPos;
    Transform roomBackground;
    public float lookAhead;

    private void LateUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (target==null)
        {
            return;
        }

        if (target.GetComponent<CharacterMovement>().facingRight)
            offset.x = lookAhead;
        else offset.x = -lookAhead;

        if(target.GetComponentInChildren<ArmRotation>().enabled)
        {
            int dir = target.GetComponentInChildren<ArmRotation>().dir;

            if (dir == 1)
                offset.y = lookAhead;
            else if (dir == 5)
                offset.y = -lookAhead;
            else offset.y = 0;
        }

        nextPos = target.position + offset;

        smoothedPosition = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, smoothTime * Time.deltaTime * 60);

        boundaryMax = GameObject.FindWithTag("Boundary/Max").transform.position;
        boundaryMin = GameObject.FindWithTag("Boundary/Min").transform.position;
        roomBackground = GameObject.FindWithTag("RoomBackground").GetComponent<Transform>();


        if (smoothedPosition.x > boundaryMax.x)
            smoothedPosition.x = boundaryMax.x;
        else if (smoothedPosition.x < boundaryMin.x)
            smoothedPosition.x = boundaryMin.x;

        if (smoothedPosition.y > boundaryMax.y)
            smoothedPosition.y = boundaryMax.y;
        else if (smoothedPosition.y < boundaryMin.y)
            smoothedPosition.y = boundaryMin.y;

        transform.position = smoothedPosition;
        roomBackground.position = new Vector2(transform.position.x, transform.position.y);
    }
}

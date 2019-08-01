using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targets;
    Transform camPos;
    public Vector2 boundaryMin, boundaryMax;
    float holdposX;
    public float smoothTime;
    public Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Vector3 smoothedPosition;
    Vector3 nextPos;
    Transform roomBackground;
    public float lookAhead;

    private void LateUpdate()
    {
        //--------------------------------------------------------------------------

        if (targets == null)
        {
            if (GameMaster.gameMode == 3)
                initTargets(true);
            else initTargets(false);

            return;
        }

        Vector3 centerPoint = cPoint();       

        //--------------------------------------------------------------------------

        if (GameMaster.gameMode != 3)
        {

            offset.x = targets[0].GetComponent<CharacterMovement>().facingRight ? lookAhead : -lookAhead;
           
            if (targets[0].GetComponentInChildren<ArmRotation>().enabled)
            {
                int dir = targets[0].GetComponentInChildren<ArmRotation>().dir;

                if (dir == 1)
                    offset.y = lookAhead;
                else if (dir == 5)
                    offset.y = -lookAhead;
                else offset.y = 0;
            }
        }

        //--------------------------------------------------------------------------

        nextPos = centerPoint + offset;

        smoothedPosition = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, smoothTime * Time.deltaTime * 60);

        boundaryMax = GameObject.FindWithTag("Boundary/Max").transform.position;
        boundaryMin = GameObject.FindWithTag("Boundary/Min").transform.position;
        roomBackground = GameObject.FindWithTag("RoomBackground").GetComponent<Transform>();

        //--------------------------------------------------------------------------

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

    //--------------------------------------------------------------------------

    Vector3 cPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 1; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    //--------------------------------------------------------------------------

    public void initTargets(bool multiPlayer)
    {
        if(!multiPlayer)
        {
            targets = new Transform[1];
            targets[0] = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        targets = new Transform[2];

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].transform;
        }
    }
}

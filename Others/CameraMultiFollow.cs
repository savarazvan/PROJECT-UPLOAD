using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraMultiFollow : MonoBehaviour
{
    public Transform[] players;
    public Vector3 offset;

    private Vector3 velocity;

    public float smoothTime = .5f, minZoom=10, maxZoom=50, zoomLimit=50;

    private Camera cam;

    private void LateUpdate()
    {
        Vector3 centerPoint = cPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        cam = GetComponent<Camera>();
    }


    Vector3 cPoint()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        
        for(int i=1; i<players.Length; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        zooming(bounds.size.x);

        return bounds.center;
    }

    void zooming(float bWidth)
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, bWidth/zoomLimit);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }
}

using UnityEngine;

public class RotatingLasers : MonoBehaviour
{
    private LineRenderer[] lasers;
    public float rotationSpeed=1f;
    private float tempRotZ = 1, changeDir = 7, lastChange=0;
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        if(Time.time>lastChange + changeDir * ((RNG.Rng() % 5) + 1))
        {
            int rng = (RNG.Rng() % 2) + 1;
            rotationSpeed = rng == 1 ? rotationSpeed * 1 : rotationSpeed * -1;
            lastChange = Time.time;
        }
    }
    
}

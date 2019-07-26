using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Vector2 floorPos, middlePos, rightPos, leftPos;

	public GameObject[] floors;
	public GameObject[] middlePlatforms;
	public GameObject[] sidePlatforms;

    public static GameObject[] objs = new GameObject[4];

    private void Start()
    {
        generateMap();
    }

    public void generateMap()
    {
        cleanMap();

        var floor = Instantiate(floors[Random.Range(0, floors.Length)], transform);
        var middle = Instantiate(middlePlatforms[Random.Range(0, middlePlatforms.Length)], transform);
        var lSide = Instantiate(sidePlatforms[Random.Range(0, middlePlatforms.Length)], transform);
        var rSide = Instantiate(sidePlatforms[Random.Range(0, middlePlatforms.Length)], transform);

        floor.transform.position = floorPos;
        middle.transform.position = middlePos;
        lSide.transform.position = leftPos;
        rSide.transform.position = rightPos;

        objs[0] = floor;
        objs[1] = middle;
        objs[2] = lSide;
        objs[3] = rSide;
    }

    void cleanMap()
    {
        for (int i = 0; i < objs.Length; i++)
            Destroy(objs[i]);
    }
}

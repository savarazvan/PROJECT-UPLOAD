using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPress : MonoBehaviour
{
    float moveDownSpeed;
    Rigidbody2D[] children;
    [SerializeField] int pointer = 1;
    public float moveForce=10f;
    public float maxDownPos=-24f;
    private bool isDown, isUp;
    public int nrOfObj;
    private float initialPos;
    int previousPointer;
    private void Start()
    {
        children = GetComponentsInChildren<Rigidbody2D>();
        initialPos = children[1].gameObject.GetComponent<Transform>().position.y;
    }

    private void Update()
    {
       /* if (pointer == nrOfObj)
        {
            previousPointer = nrOfObj - 1;
            pointer = 1;
        }*/       
        if(children[pointer].GetComponent<Transform>().position.y!=maxDownPos)
        moveDown(pointer);
        if(isDown)
        moveUp(pointer);
        if (isDown && isUp)
        {
            isDown = false;
            isUp = false;
            previousPointer = pointer;
            while(pointer==previousPointer)
            pointer = ((RNG.Rng()) % nrOfObj) + 1;
        }
    }

    void moveDown(int pointer)
    {
        children[previousPointer].velocity = new Vector2(0, 0);
        if (isDown)
            return;
        float force = moveForce;
        force = force * -1;
        children[pointer].velocity= new Vector2(0, force*Time.deltaTime*1000f);
        if (children[pointer].transform.localPosition.y<=maxDownPos)
        {
            isDown = true;
        }
    }

    void moveUp(int pointer)
    {
        if (isUp)
            return;
        float force = moveForce;
        children[pointer].velocity = new Vector2(0, force*Time.deltaTime*100);
        if (children[pointer].GetComponent<Transform>().position.y >= initialPos)
        {
            force = 0;
            isUp = true;
        }
            
    }
}

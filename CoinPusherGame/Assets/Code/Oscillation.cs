using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public Rigidbody objectToMove;
    public float speed;
    public float motionLimit;
    
    // Start is called before the first frame update
    void Start()
    {
        objectToMove = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.PingPong(Time.time * speed, 1) * motionLimit;
        objectToMove.MovePosition(new Vector3(0, 0, z));
    }
}

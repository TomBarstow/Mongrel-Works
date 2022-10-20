using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody spawner;
    public Rigidbody dropped;
    public float motionLimit = 4.5F;

    public int CoinCount = 20;
    public float dropRate = 0.5F;
    private float nextDrop = 0.0F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextDrop)
        {
            if (CoinCount >= 0)
            {
                DropCoin();
            }
        }

        if (Input.GetKey(KeyCode.A) && spawner.position.x > -(motionLimit))
        {
            spawner.transform.Translate(-0.05f, 0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.D) && spawner.position.x < motionLimit)
        {
            spawner.transform.Translate(0.05f, 0f, 0.0f);
        }
    }
    void DropCoin()
    {
        //CoinCount(-1);

        nextDrop = Time.time + dropRate;
        Instantiate(dropped, spawner.transform.position, spawner.transform.rotation);

    }
}
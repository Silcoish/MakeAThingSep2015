using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {

    public float rotAmountPerSecond = 6;
    Transform t;

    void Start()
    {
        t = transform;
    }

    void Update()
    {
        t.Rotate(new Vector3(0f, 0f, -rotAmountPerSecond * Time.deltaTime));
    }
}

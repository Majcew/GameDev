using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Rigidbody rbody;
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.AddTorque(new Vector3(15, 30, 45));
    }
    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}

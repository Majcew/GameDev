using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitator : MonoBehaviour
{
    private float hoverForce = 30f;
    private float hoverHeight = 1.5f;
    private float powerInput;
    private float turnInput;
    Rigidbody rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        hoverForce = Random.Range(30.0f, 60.0f);
        hoverHeight = Random.Range(0.7f, 1.3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, hoverHeight))
        {
            if(hit.collider.name.Equals("Ground"))
            {
                float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
                rbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
            }
        }
    }
}

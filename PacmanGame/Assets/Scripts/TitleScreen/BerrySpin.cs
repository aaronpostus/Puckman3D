using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySpin : MonoBehaviour
{
    [SerializeField] GameObject berry;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationAxis;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        this.transform = berry.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}

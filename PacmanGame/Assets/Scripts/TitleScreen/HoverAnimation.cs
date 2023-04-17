using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    [SerializeField] GameObject gameObj;
    [SerializeField] float height;
    [SerializeField] float speed;
    private bool direction;
    private Transform transform;
    private float initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        this.transform = gameObj.transform;
        this.direction = (Random.Range(0,2) == 0);
        this.initialPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction) {
            if(transform.position.y > initialPosition + height) {
                direction = !direction;
            }
            else {
                transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
            }
        }
        else {
            if(transform.position.y < initialPosition - height) {
                direction = !direction;
            }
            else {
                transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime), transform.position.z);
            }
        }
    }
}

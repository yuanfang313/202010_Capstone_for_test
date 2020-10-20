using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailMove : MonoBehaviour
{
    public float speed;

    private Transform rightAnswerTransform;
    private Transform trailObjectTransform;
    private GameObject trailObject;
    private Vector3 target;

    void Start()
    {
        
        rightAnswerTransform = GameObject.FindGameObjectWithTag("rightAnswerPosition").transform;
        target = new Vector3(rightAnswerTransform.position.x, rightAnswerTransform.position.y, rightAnswerTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (rightAnswerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightAnswerTransform.position, speed * Time.deltaTime);
            Destroy(gameObject, 1);
        }
        else
        {
            Destroy(gameObject);
        }

        /*if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            DestroyReminderObject();
        }*/
    }

}

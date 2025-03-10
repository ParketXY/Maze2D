using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}

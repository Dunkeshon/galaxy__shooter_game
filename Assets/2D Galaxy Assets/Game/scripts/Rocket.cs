using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float _speed = 10f;
    public static int damage =5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if((transform.position.y>5.42f)||(transform.position.y<-5.51f))
        {
            Destroy(this.gameObject);
        }
    }
}

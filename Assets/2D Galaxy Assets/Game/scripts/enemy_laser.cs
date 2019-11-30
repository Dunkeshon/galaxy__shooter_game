using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_laser : MonoBehaviour
{
    public static int damage = 1;
    [SerializeField]
    private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime);
       if (transform.position.y < -5.51f)
        {
            Destroy(this.gameObject);
        }
    }
}

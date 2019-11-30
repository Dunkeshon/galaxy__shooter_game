using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_power : MonoBehaviour
{
    private float _speed = 1f;
    [SerializeField]
    private AudioClip _clip;
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Destroying_Powers();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player player = other.GetComponent<player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                player.RefreshLifes();
                player.Restore_engine();
            }
            Destroy(this.gameObject);
        }
    }
    private void Destroying_Powers()
    {
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
        if (_gm.GameOver)
        {
            Destroy(this.gameObject);
        }
    }
}

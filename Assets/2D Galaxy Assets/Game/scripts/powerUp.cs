using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _PowerUPId;
    private GameManager _gameManager;
    [SerializeField]
    private AudioClip _clip;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        Destroying_Powers();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player player = other.GetComponent<player>();
            if(player!=null)
            {
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                if (_PowerUPId == 0)//tripple shot
                {
                    player.TripleShotPowerUpOn();
                }
                else if(_PowerUPId==1)//speed
                {
                   player.SpeedUpOn();
                }
                else if(_PowerUPId==2)
                {
                    player.ShieldPowerOn(); // shield;
                }
            }
            Destroy(this.gameObject);
        }
    }
    private void Destroying_Powers()
    {
        if(transform.position.y<-7f)
        {
            Destroy(this.gameObject);
        }
        if (_gameManager.GameOver)
        {
            Destroy(this.gameObject);
        }
    }

}

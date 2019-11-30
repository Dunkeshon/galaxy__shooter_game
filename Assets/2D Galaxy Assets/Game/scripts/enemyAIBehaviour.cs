using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAIBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _canshoot = 0f;
    [SerializeField]
    private float _shootingRate = 1f;
    public int life = 1;
    [SerializeField]
    private GameObject _explosion;
    public int enemyScore = 10;
    private UI_manager _UImanager;
    private GameManager _gameManager;
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private GameObject _playerHeal;
    // Start is called before the first frame update
    void Start()
    {
        _UImanager = GameObject.Find("Canvas").GetComponent<UI_manager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        moving();
        shooting();
        Destroying_when_Over();


    }
    private void moving()
    {
        float randomX = Random.Range(-7.95f, 7.95f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.28f)
        {
            transform.position = new Vector3(randomX, 6.27f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="laser")
        {
            laser laser = other.GetComponent<laser>();
            if (laser != null)
            {
                Damage();
                Destroy(laser.gameObject);
            }
        }
        else if (other.tag == "Player")
        {
            player player = other.GetComponent<player>();
            if (player != null)
            {
                Damage_hit();
                player.Damage();
            }
        }
        else if (other.tag=="Rocket")
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket!=null)
            {
                damageRocket();
                Destroy(rocket.gameObject);
            }
        }
    }
    private void shooting()
    {
        if (Time.time > _canshoot)
        {
              Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.97f, 0), Quaternion.identity);
            _canshoot = _shootingRate + Time.time;
        }
    
    }
   public void Damage()
    {
        life=life - laser.damage;
        CheckForDeath();
    }
    public void Damage_hit()
    {
        life = 0;
        CheckForDeath();
    }

    private void Destroying_when_Over()
    {
        if (_gameManager.GameOver)
            { 
                Destroy(this.gameObject);
            }
    }
    private void GenerateHeal()
    {
        int randomx = Random.Range(0, 7);
        if(randomx==5)
        {
            Instantiate(_playerHeal, transform.position, Quaternion.identity);
        }
    }
    public void damageRocket()
    {
        life = life - Rocket.damage;
        CheckForDeath();
    }
    private void CheckForDeath()
    {
        if (life < 1)
        {
            GenerateHeal();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _UImanager.UpdateScore(enemyScore);
        }
    }
}

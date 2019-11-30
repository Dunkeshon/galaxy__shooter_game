using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [SerializeField]
    private GameObject _Shield;
    [SerializeField]
    private GameObject _Shield_power_Up;
    public bool ShieldIsActive = false;
    [SerializeField]
    private GameObject _explosion;
    public int life = 3;
    public bool haveSpeedUp = false;
    public bool canTripleShot = false;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private float _firerate = 0.25f;
    private float _canfire = 0f;
    private UI_manager _UImanager;
    private GameManager _gameManager;
    private Spawn_manager_script _spawnManager;
    private AudioSource _audiosource;
    [SerializeField]
    private GameObject []_engine_hurt;
    private  PlayerAnim _playerAnimation;
    [SerializeField]
    private AudioClip _player_damage;
    [SerializeField]
    private GameObject _RocketPrefab;
    private float _firerateRocket = 1f;
    private float _canfireRocket = 0f;
    public bool wechooselaser = true;
    [SerializeField]
    private AudioClip _rocketsound;
    private Joystick joystick;
    private Shooting_button_script shooting_Button_Script;
    private change_button_script change_Button_Script;
    [HideInInspector]
    public bool _need_to_find=true;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _UImanager = GameObject.Find("Canvas").GetComponent<UI_manager>();
        if(_UImanager!=null)
        {
            _UImanager.Updatelives(life);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("_Spawn_manager").GetComponent<Spawn_manager_script>();
        if(_spawnManager!=null)
        {
            _spawnManager.StartSpawnRoutine();
        }
        _audiosource = GetComponent<AudioSource>();
        _playerAnimation = GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_need_to_find)
        {
            if(!_gameManager.GameOver)
            {
                joystick = FindObjectOfType<Joystick>();
                shooting_Button_Script = FindObjectOfType<Shooting_button_script>();
                change_Button_Script = FindObjectOfType<change_button_script>();
                _need_to_find = false;
            }
        }
        Movement();
        if ((Input.GetKey(KeyCode.Space))||(shooting_Button_Script.pressed))
        {
            Shooting();
        }
        if(Input.GetKeyDown(KeyCode.R) ||(change_Button_Script.pressed))
        {
            ChangeWeapons();
        }
        if(_gameManager.GameOver)
        {
            _need_to_find = true;
        }
    }
    public void Shooting()
    {
        if (wechooselaser)
        {
            Shootlaser();
        }
        else if (!wechooselaser)
        {
            Shootrocket();
        }
    }
    private void Shootlaser()
    {
        if (Time.time > _canfire)
        {
            _audiosource.Play();
            if (canTripleShot)
            {
                Instantiate(_LaserPrefab, transform.position + new Vector3(-0.56f, 0, 0), Quaternion.identity);
                Instantiate(_LaserPrefab, transform.position + new Vector3(0.56f, 0, 0), Quaternion.identity);
                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            else if (!canTripleShot)
            {

                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _canfire = _firerate + Time.time;
        }
    }
    private void Shootrocket()
    {
        if (Time.time > _canfireRocket)
        {
            AudioSource.PlayClipAtPoint(_rocketsound, Camera.main.transform.position);
            if (canTripleShot)
            {
                Instantiate(_RocketPrefab, transform.position + new Vector3(-0.56f, 0, 0), Quaternion.identity);
                Instantiate(_RocketPrefab, transform.position + new Vector3(0.56f, 0, 0), Quaternion.identity);
                Instantiate(_RocketPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            else if (!canTripleShot)
            {

                Instantiate(_RocketPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _canfireRocket = _firerateRocket + Time.time;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); 
        float horizontalJoyMove = 0f;
        float verticalJoyMove = 0f;
        horizontalJoyMove = joystick.Horizontal * _speed * Time.deltaTime;
        verticalJoyMove = joystick.Vertical * _speed * Time.deltaTime;
        if (haveSpeedUp==true)
        {
            transform.Translate(Vector3.right * horizontalJoyMove* boosts_profile.speed_increase);
            transform.Translate(Vector3.up * verticalJoyMove* boosts_profile.speed_increase);
            transform.Translate(Vector3.right * horizontalInput * _speed * boosts_profile.speed_increase * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * boosts_profile.speed_increase * Time.deltaTime);
        }
        else if (!haveSpeedUp)
        {
            transform.Translate(Vector3.right * horizontalJoyMove);
            transform.Translate(Vector3.up * verticalJoyMove);
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, 0);
            }
            if (transform.position.y < -4.2f)
            {
                transform.position = new Vector3(transform.position.x, -4.2f);
            }
            if (transform.position.x > 9f)
            {
                transform.position = new Vector3(-9f, transform.position.y);
            }
            if (transform.position.x < -9f)
            {
                transform.position = new Vector3(9f, transform.position.y);
            }
        // animation
        if (horizontalInput == 0)
        {
            _playerAnimation.IsIdle();
        }
        else if (horizontalInput < 0)
        {
            _playerAnimation.IsMovingLeft();
        }
        else
        {
            _playerAnimation.IsMovingRight();
        }

        if (horizontalJoyMove == 0)
        {
            _playerAnimation.IsIdle();
        }
        else if (horizontalJoyMove < 0)
        {
            _playerAnimation.IsMovingLeft();
        }
        else
        {
            _playerAnimation.IsMovingRight();
        }

    }
    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public void SpeedUpOn()
    {
        haveSpeedUp = true;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    public IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        haveSpeedUp = false;
    }
    public void ShieldPowerOn()
    {
        ShieldIsActive = true;
        _Shield.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "enemy_laser")
        {
            Damage();
        }
    }
    public void Damage()
    {
        if (ShieldIsActive)
        {
            int shield = boosts_profile.shield_deffence;
            shield = shield - enemy_laser.damage;
            if(shield<1)
            {
                ShieldIsActive = false;
                _Shield.SetActive(false);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(_player_damage, Camera.main.transform.position);
            life = life - enemy_laser.damage;
            Engine_failure();

        }

        _UImanager.Updatelives(life);

        if (life < 1)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity); 
            _UImanager.UpdateHighScore();
            _gameManager.GameOver = true;
            _UImanager.ShowTitleScreen();

            Destroy(this.gameObject);

        }
    }
    private void Engine_failure()
    {
        if (life == 2)
        {
            int random_engine = Random.Range(0, 2);
            _engine_hurt[random_engine].SetActive(true);
        }
        if (life == 1)
        {
            if (_engine_hurt[0].activeSelf)
            {
                _engine_hurt[1].SetActive(true);
            }
            else
            {
                _engine_hurt[0].SetActive(true);
            }
        }
    }
    public void Restore_engine()
    {
        for(int i=0;i<2;i++)
        {
            _engine_hurt[i].SetActive(false);
        }
    }
    public void RefreshLifes()
    {
        life = 3;
        _UImanager.Updatelives(life);
    }
    public void ChangeWeapons()
    {

        if (wechooselaser ==true)
        {
            wechooselaser = false;
        }
        else
        {
            wechooselaser = true;
        }
    }
   
}

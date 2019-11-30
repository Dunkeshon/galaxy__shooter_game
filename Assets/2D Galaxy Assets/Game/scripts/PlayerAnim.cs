using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator _anim; 
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    // reference exist in player script
    public void IsMovingLeft()
    {
        _anim.SetBool("Turn_left", true);
        _anim.SetBool("Turn_right", false);
    }

    public void IsMovingRight()
    {
        _anim.SetBool("Turn_right", true);
        _anim.SetBool("Turn_left", false);
    }

    public void IsIdle()
    {
        _anim.SetBool("Turn_left", false);
        _anim.SetBool("Turn_right", false); 
    }

}

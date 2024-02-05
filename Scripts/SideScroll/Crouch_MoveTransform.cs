using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Crouch_MoveTransform : MonoBehaviour
{
    [SerializeField]private Player_Controller player;
    public Vector3 standingLocalPos = Vector3.zero;
    public Vector3 crouchingLocalPos = new Vector3(0, 0.5f, 0);
    private void Awake()
    {
        player.OnCrouch += OnCrouch;
    }
    void OnCrouch(bool value)
    {
        if (value)
        {
            transform.localPosition = crouchingLocalPos;
        }
        else
        {
            transform.localPosition = standingLocalPos;
        }
    }
}

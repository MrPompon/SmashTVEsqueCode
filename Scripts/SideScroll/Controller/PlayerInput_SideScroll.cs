using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player_SideScroll))]
public class PlayerInput_SideScroll : MonoBehaviour {

	Player_SideScroll player;

	void Start () {
		if (player == null)
		{
			player = GetComponent<Player_SideScroll> ();
		}
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown("Jump"))
        {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp ("Jump"))
        {
			player.OnJumpInputUp ();
		}
	}
}

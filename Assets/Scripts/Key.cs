using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
	public Player m_player;

	public override void Interact()
	{
		m_player.m_hasKey = true;
	}
}

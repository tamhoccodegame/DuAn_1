using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
	
	public enum RuneType
	{
		Speed,
		Damage,
		DoubleJump,
		AttackSpeed,
	}

	private RuneType runeType;

	public Rune(RuneType _runeType)
    {
		runeType = _runeType;
    }

    public Sprite GetSprite()
	{
		return RuneAssets.instance.attackRuneSprite; //this is ex, not offical
	}

}

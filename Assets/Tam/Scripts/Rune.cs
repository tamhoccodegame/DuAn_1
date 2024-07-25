using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
	
	public enum RuneType
	{
		Dash,
		Damage,
		DoubleJump,
		AttackSpeed,
	}

	public RuneType runeType;

	public Rune(RuneType _runeType)
    {
		runeType = _runeType;
    }

    public Sprite GetSprite()
	{
		switch(runeType)
		{
			default: 
			case RuneType.Damage:			return RuneAssets.instance.damageRuneSprite;
			case RuneType.DoubleJump:		return RuneAssets.instance.doublejumpRuneSprite;
		}
	}

	public int GetCost()
	{
		switch(runeType)
		{
			default:
			case RuneType.Damage: return 1;
		}
	}

}

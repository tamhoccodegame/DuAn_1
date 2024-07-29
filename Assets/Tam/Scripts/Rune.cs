using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : IShopItem
{
	
	public enum RuneType
	{
		Dash,
		Damage,
		DoubleJump,
		AttackSpeed,
		Fire,
		Electric,
		Ice,
		Lifesteal,
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
			case RuneType.Damage:			return GameAssets.instance.damageRuneSprite;
			case RuneType.DoubleJump:		return GameAssets.instance.doublejumpRuneSprite;
			case RuneType.Dash:				return GameAssets.instance.dashRuneSprite;
			case RuneType.Fire:				return GameAssets.instance.fireRuneSprite;
			case RuneType.Electric:         return GameAssets.instance.electricRuneSprite;
			case RuneType.Ice:				return GameAssets.instance.iceRuneSprite;
			case RuneType.Lifesteal:		return GameAssets.instance.lifestealRuneSprite;
		}
	}

	public int GetCost()
	{
		switch(runeType)
		{
			default:
			case RuneType.Damage: return 2000;
			case RuneType.Fire: return 5000;
			case RuneType.Electric: return 10000;
			case RuneType.Ice: return 10000;
			case RuneType.Lifesteal: return 20000;
		}
	}

	public string GetName()
	{
		return runeType.ToString();
	}
	public string GetDescription()
	{
		switch(runeType)
		{
			default: 
			case RuneType.DoubleJump: return "This Rune help your legs strong, can jump 2 times";
			case RuneType.Damage:	return  "This Rune help you to slash stronger";
		}
	}
}

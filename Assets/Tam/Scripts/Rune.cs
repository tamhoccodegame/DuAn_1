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
			case RuneType.Damage: return 1000;
			case RuneType.Fire: return 5000;
			case RuneType.Electric: return 2500;
			case RuneType.Ice: return 3000;
			case RuneType.Lifesteal: return 15000;
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
			case RuneType.Electric:	return "This Rune help you attack with LIGHTNING effect";
			case RuneType.Ice:		return "This Rune help you FROZEN you enemies";
			case RuneType.Lifesteal: return "This Rune help you live longer";
			case RuneType.Fire: return "This Rune will IGNITE your enemies";
		}
	}
}

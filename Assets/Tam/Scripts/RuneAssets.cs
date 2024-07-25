using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAssets : MonoBehaviour
{
    public static RuneAssets instance;
    public Sprite damageRuneSprite;
	public Sprite doublejumpRuneSprite;

	private void Awake()
	{
		instance = this;
	}

}

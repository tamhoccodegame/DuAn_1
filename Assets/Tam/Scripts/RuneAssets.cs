using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAssets : MonoBehaviour
{
    public static RuneAssets instance;
    public Sprite attackRuneSprite;

	private void Awake()
	{
		instance = this;
	}

}

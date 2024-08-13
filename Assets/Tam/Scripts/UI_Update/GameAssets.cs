using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

	[Header("Rune Sprites")]
    public Sprite damageRuneSprite;
	public Sprite doublejumpRuneSprite;
	public Sprite dashRuneSprite;
	public Sprite fireRuneSprite;
	public Sprite electricRuneSprite;
	public Sprite iceRuneSprite;
	public Sprite lifestealRuneSprite;

	[Header("Item Sprites")]
	public Sprite helmetSprite;
	public Sprite armorSprite;
	public Sprite bootSprite;
	public Sprite swordSprite;

	private void Awake()
	{
		instance = this;
	}

}

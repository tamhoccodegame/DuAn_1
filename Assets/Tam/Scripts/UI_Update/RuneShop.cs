using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class RuneShop : MonoBehaviour
{
    public UI_Market uiMarket;
    private static List<Rune> runes;
    // Start is called before the first frame update
    void Start()
    {
        runes = new List<Rune>()
        {
            new Rune(Rune.RuneType.Lifesteal),
			new Rune(Rune.RuneType.Fire),
            new Rune(Rune.RuneType.Ice),
            new Rune(Rune.RuneType.Electric),
            new Rune(Rune.RuneType.Damage),
		};
        //Add rune to runeshop

        List<IShopItem> shopItems = new List<IShopItem>();
        foreach (Rune rune in runes)
        {
            shopItems.Add(rune);
        }
        uiMarket.RefreshShop(shopItems);
    }

}

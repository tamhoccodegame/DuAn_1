using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItem
{
    string GetName();
    int GetCost();
    Sprite GetSprite();
    string GetDescription();
}

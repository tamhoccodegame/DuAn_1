using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public event EventHandler OnInventoryChange;

    private List<Rune> runes;

    public Inventory()
    {
        runes = new List<Rune>();
    }

    public void AddRune(Rune rune)
    {
        runes.Add(rune);
        OnInventoryChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveRune(Rune rune)
    {
        runes.Remove(rune);
		OnInventoryChange?.Invoke(this, EventArgs.Empty);
	}

    public bool IsContain(Rune rune)
    {
        return runes.Contains(rune);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Equipment
{
	public event EventHandler OnEquipmentChange;
	private List<Rune> equipment;
	private int maxSlot;
	private int avaSlot;

	public Equipment()
	{
		equipment = new List<Rune>();
		maxSlot = 1;
		avaSlot = 1;
	}
	public void IncreaseSlot(int slot)
	{
		maxSlot++;
		avaSlot++;
		OnEquipmentChange?.Invoke(this, EventArgs.Empty);
	}

	private bool CanEquip()
	{
		return avaSlot > 0;
	}

	public void Equip(Rune rune)
	{
		if(CanEquip())
		{
			equipment.Add(rune);
			avaSlot--;
			OnEquipmentChange?.Invoke(this, EventArgs.Empty);
		}
	}

	public void Unequip(Rune rune)
	{
		equipment.Remove(rune);
		avaSlot++;
		OnEquipmentChange?.Invoke(this, EventArgs.Empty);
	}



}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Equipment
{
	public event EventHandler OnEquipmentChange;
	private List<Rune> equipments;
	private int maxSlot;
	private int avaSlot;

	public Equipment()
	{
		equipments = new List<Rune>();
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
			equipments.Add(rune);
			avaSlot--;
			OnEquipmentChange?.Invoke(this, EventArgs.Empty);
		}
	}

	public void Unequip(Rune rune)
	{
		equipments.Remove(rune);
		avaSlot++;
		OnEquipmentChange?.Invoke(this, EventArgs.Empty);
	}

	public List<Rune> GetEquipmentList()
	{
		return equipments;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObejcts/Item")]

[System.Serializable]
public class ItemSO : ScriptableObject
{
    public int id;
    public new string name;
    public Type itemType;
    public string description;
    public Sprite iconUI;
    public Sprite icon;
    public List<ItemSO> recipe;
    public List<ItemSO> componentIn;

    public enum Type
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Quest,
        Market
    }
}
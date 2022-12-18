using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTier{
    Magic,
    Physic
}

public enum WeaponType{
    Main,
    Secondary
}

public enum WeaponRarity{
    Common,
    Rare,
    Epic,
    Legendary
}

public enum WeaponRange{
    Melee,
    Fight, // Pugilat
    Range, //Distance
}

public enum WeaponDurability{
    Unsused, //Neuf
    Used, // Usee
    VeryUsed, // Tres Usee
    Brittle, // Fragile
    Broken // Casse

}

[System.Serializable]
public struct Weapon {
    public string weaponName;
    public Sprite weaponSprite;
    public int weaponPrice;
    public WeaponTier weaponTier;
    public WeaponRarity weaponRarity;
    public WeaponType weaponType;
    public WeaponRange weaponRange;
    public float weaponWeight;
    public float weaponDamage;
    public float weaponPenetration;
    public float weaponSpeed;
    public WeaponDurability weaponDurability;
    
}

[CreateAssetMenu(menuName = "WeaponCarrousel/WeaponData")]
public class WeaponData : ScriptableObject
{
    public List<Weapon> weapons;
}

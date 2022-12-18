using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVisualizer : MonoBehaviour
{
    public static Color orange = new Color(255, 127, 39);

    [Header("References")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject weaponVisualizer;

    [SerializeField] WeaponData allWeaponData;
    
    [Header("Params References")]
    [SerializeField] private Text nameValue;
    [SerializeField] private Image sprValue;
    [SerializeField] private Text tierValue;
    [SerializeField] private Text rarityValue;
    [SerializeField] private Text typeValue;
    [SerializeField] private Text rangeValue;
    [SerializeField] private Text priceValue;
    [SerializeField] private Text weightValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text equipedValue;

    private int indexCarousel;

    private Weapon equipedWeapon;
    private Weapon currentWeapon;

    void Start(){
        indexCarousel = 0;
        equipedWeapon = allWeaponData.weapons[0];
        currentWeapon = equipedWeapon;
        UpdateUI();
    }

    public void ShowWeaponVisualizer(){
        mainMenu.SetActive(false);
        weaponVisualizer.SetActive(true);
    }

    public void BackToMenu(){
        weaponVisualizer.SetActive(false);
        mainMenu.SetActive(true);
    }

    //Update all text ui to the currentWeapon
    public void UpdateUI(){
        nameValue.text = currentWeapon.weaponName;
        
        sprValue.sprite = currentWeapon.weaponSprite;

        tierValue.text = TierName(currentWeapon.weaponTier);
        
        var (rarityName, rarityColor) = RarityNameAndColor(currentWeapon.weaponRarity);
        rarityValue.text = rarityName;
       
        rangeValue.color = rarityColor;

        typeValue.text = TypeName(currentWeapon.weaponType);
       
        rangeValue.text = RangeName(currentWeapon.weaponRange);

        priceValue.text = currentWeapon.weaponPrice + " PO";
        priceValue.color = Color.yellow;

        weightValue.text = currentWeapon.weaponWeight + " Kg";

        damageValue.text = currentWeapon.weaponDamage.ToString();

        if(currentWeapon.weaponName == equipedWeapon.weaponName)
            equipedValue.text = "Equipe";
        else
            equipedValue.text = "Non Equipe";

    }

    public void NextWeapon(){
        indexCarousel++;

        if(indexCarousel > allWeaponData.weapons.Count-1){
            indexCarousel =  0;
        }
        if(indexCarousel < 0){
            indexCarousel = allWeaponData.weapons.Count - 1;
        }

        currentWeapon = allWeaponData.weapons[indexCarousel];
        UpdateUI();
    }

    public void PreviousWeapon(){
        indexCarousel--;

        if(indexCarousel > allWeaponData.weapons.Count-1){
            indexCarousel =  0;
        }
        if(indexCarousel < 0){
            indexCarousel = allWeaponData.weapons.Count - 1;
        }

        currentWeapon = allWeaponData.weapons[indexCarousel];
        UpdateUI();
    }

    //Return the name to WeaponTier param
    public string TierName(WeaponTier tier){
        switch (tier){
            case WeaponTier.Magic:
                return "Magic";
            case WeaponTier.Physic:
                return "Physic";
        }
        return "None";
    }

    //Return the name to WeaponType param
    public string TypeName(WeaponType type){
        switch(type){
            case WeaponType.Main:
                return "Principale";
            case WeaponType.Secondary:
                return "Secondaire";
        }

        return "None";
    }

    //Return the name to WeaponRange param
    public string RangeName(WeaponRange range){
        switch(range){
            case WeaponRange.Melee:
                return "Melee";
            case WeaponRange.Fight:
                return "Pugilat";
            case WeaponRange.Range:
                return "Distance";
        }
        
        return "None";
    }

    //Return the name and the color to WeaponDurability param
    public (string, Color) DurabilityNameAndColor(WeaponDurability durability){
        switch(durability){
            case WeaponDurability.Unsused:
                return ("Neuf", Color.green);
            case WeaponDurability.Used:
                return ("Usee", Color.yellow);
            case WeaponDurability.VeryUsed:
                return ("Tres usee", orange);
            case WeaponDurability.Brittle:
                return ("Fragile", Color.red);
            case WeaponDurability.Broken:
                return ("Casee", Color.gray);
        }

        return ("None", Color.white);
    }

    //Return the name and the color to WeaponRarity param
    public (string, Color) RarityNameAndColor(WeaponRarity rarity){
        switch(rarity){
            case WeaponRarity.Common:
                return ("Commun", Color.white);
            case WeaponRarity.Rare:
                return ("Rare", Color.blue);
            case WeaponRarity.Epic:
                return ("Epique", Color.magenta);
            case WeaponRarity.Legendary:
                return ("Legendaire", orange);
        }

        return ("None", Color.white);
    }

}

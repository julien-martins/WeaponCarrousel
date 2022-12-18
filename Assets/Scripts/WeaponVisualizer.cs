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
    [SerializeField] private Text speedValue;
    [SerializeField] private Text speedStatValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text damageStatValue;
    [SerializeField] private Text equipedValue;
    [SerializeField] private Text durabilityStr;
    [SerializeField] private Text durabilityNbr;

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
       
        typeValue.text = TypeName(currentWeapon.weaponType);
        
        priceValue.text = currentWeapon.weaponPrice + " PO";
        priceValue.color = Color.yellow;

        weightValue.text = currentWeapon.weaponWeight + " Kg";

        rangeValue.text = RangeName(currentWeapon.weaponRange);
        rangeValue.color = rarityColor;

        damageValue.text = currentWeapon.weaponDamage.ToString();
        var damageDiff = (currentWeapon.weaponDamage - equipedWeapon.weaponDamage);
        if(damageDiff > 0){
            damageStatValue.text = "+ ";
            damageStatValue.text += Mathf.Abs(damageDiff).ToString();
            damageStatValue.color = Color.green;
        } else if(damageDiff < 0){
            damageStatValue.text = "- ";
            damageStatValue.text += Mathf.Abs(damageDiff).ToString();
            damageStatValue.color = Color.red;
        } else{
            damageStatValue.text = "";
        }
        

        speedStatValue.text = currentWeapon.weaponSpeed.ToString();
        var speedDiff = (currentWeapon.weaponSpeed - equipedWeapon.weaponSpeed);
        if(speedDiff > 0){
            speedStatValue.text = "+ ";
            speedStatValue.text += Mathf.Abs(speedDiff).ToString();
            speedStatValue.color = Color.green;
        } else if (speedDiff < 0){
            speedStatValue.text = "- ";
            speedStatValue.text += Mathf.Abs(speedDiff).ToString();
            speedStatValue.color = Color.red;
        } else {
            speedStatValue.text = "";
        }
        

        var (duraStr, duraColor) = DurabilityNameAndColor(currentWeapon.weaponDurability);
        durabilityStr.text = duraStr;
        durabilityNbr.text = currentWeapon.weaponDurability.ToString();
        durabilityNbr.color = duraColor;

        if(currentWeapon.weaponName == equipedWeapon.weaponName)
            equipedValue.text = "Equipe";
        else
            equipedValue.text = "Non Equipe";

    }

    public void NextWeapon(){
        indexCarousel++;

        if(indexCarousel > allWeaponData.weapons.Count - 1){
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
    public (string, Color) DurabilityNameAndColor(int durability){

        if(durability <= 100 && durability >= 80){
            return ("Neuf", Color.green);
        }
        else if(durability <= 79 && durability >= 49){
            return ("Usee", Color.yellow);
        }
        else if(durability <= 48 && durability >= 15){
            return ("Tres Usee", orange);
        }
        else if(durability <= 14 && durability >= 1){
            return ("Fragile", Color.red);
        }
        else if(durability == 0){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    //References to parts of the UI
    public GameObject weaponWindow;
    public GameObject shieldWindow;
    public GameObject weaponContent;
    public GameObject shieldContent;
    public GameObject inventoryControls;

    //List item prefabs
    public GameObject weaponItemPrefab;
    public GameObject shieldItemPrefab;

    //Script references
    public Inventory inventoryScript;
    public Player playerScript;

    //List of character panels
    public List<GameObject> characterPanels;
    
    //Int to keep track of which character is being equipped with gear
    private int characterSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Populate the character panels with proper info
        for (int i = 0; i < characterPanels.Count; i++)
        {
            characterPanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerScript.party[i].name;
            characterPanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "HP: " + playerScript.party[i].HP;
            characterPanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "MP: " + playerScript.party[i].MP;
            characterPanels[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Attack: " + playerScript.party[i].Attack;
            characterPanels[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Magic: " + playerScript.party[i].Magic;
            characterPanels[i].transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Defense: " + playerScript.party[i].Defense;
            characterPanels[i].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Speed: " + playerScript.party[i].Speed;
            if (playerScript.party[i].cur_w != -1)
            {
                if (!inventoryScript.w_list[playerScript.party[i].cur_w].equipped)
                {
                    inventoryScript.equip_weapon(playerScript.party[i].cur_w, playerScript.party[i]);
                    characterPanels[i].transform.GetChild(7).GetComponent<Image>().sprite = inventoryScript.w_list[playerScript.party[i].cur_w].image;
                    characterPanels[i].transform.GetChild(7).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    playerScript.party[i].cur_w = -1;
                }

            }
            if (playerScript.party[i].cur_s != -1)
            {
                if (!inventoryScript.s_list[playerScript.party[i].cur_s].equipped)
                {
                    inventoryScript.equip_shield(playerScript.party[i].cur_s, playerScript.party[i]);
                    characterPanels[i].transform.GetChild(8).GetComponent<Image>().sprite = inventoryScript.s_list[playerScript.party[i].cur_s].image;
                    characterPanels[i].transform.GetChild(8).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    playerScript.party[i].cur_s = -1;
                }
            }
        }
    }

    //Open weapon tab and load weapons
    public void WeaponWindowOpened(int characterNumber)
    {
        //Keep track of which character's weapon is being swapped
        characterSelected = characterNumber;

        //Loop through weapons
        if (!weaponWindow.activeInHierarchy)
        {
            foreach (Weapon w in inventoryScript.w_list)
            {
                if (!w.equipped)
                {
                    //Instantiate new weapon UI item
                    GameObject newWeapon = Instantiate(weaponItemPrefab, weaponContent.transform);

                    //Set up button method
                    newWeapon.GetComponent<Button>().onClick.AddListener(() => EquipWeapon(newWeapon.transform.GetChild(0).gameObject));

                    //Change info on UI to match that of the weapon in the list
                    newWeapon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = w.name;
                    newWeapon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Attack: " + w.stat;
                    newWeapon.transform.GetChild(2).GetComponent<Image>().sprite = w.image;
                }
            }
        }

        //Show weapon window
        weaponWindow.SetActive(true);
    }

    //Open and load the shield tab
    public void ShieldWindowOpened(int characterNumber)
    {   
        //Keep track of which character is swapping shield
        characterSelected = characterNumber;

        //Loop through and populate shield list (same implementation as weapon list)
        if (!shieldWindow.activeInHierarchy)
        {
            foreach (Shield s in inventoryScript.s_list)
            {
                if (!s.equipped)
                {
                    GameObject newShield = Instantiate(shieldItemPrefab, shieldContent.transform);

                    newShield.GetComponent<Button>().onClick.AddListener(() => EquipShield(newShield.transform.GetChild(0).gameObject));

                    newShield.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s.name;
                    newShield.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Defense: " + s.stat;
                    newShield.transform.GetChild(2).GetComponent<Image>().sprite = s.image;
                }
            }
        }
        
        shieldWindow.SetActive(true);
    }

    //Button method for equipping weapon
    public void EquipWeapon(GameObject weaponName)
    {
        //Keep track of which weapon in the list is being equipped
        int weaponIndex = 0;

        //Loop through weapon list
        for (int i = 0; i < inventoryScript.w_list.Count; i++)
        {   
            //Verify button clicked matches the weapon in the list by checking the name
            if  (inventoryScript.w_list[i].name == weaponName.GetComponent<TextMeshProUGUI>().text)
            {
                //Set index
                weaponIndex = i;

                //Change sprite on character panel
                characterPanels[characterSelected].transform.GetChild(7).GetComponent<Image>().sprite = inventoryScript.w_list[i].image;
            }
        }

        //Equip weapon through inventory script
        inventoryScript.equip_weapon(weaponIndex, playerScript.party[characterSelected]);
        characterPanels[characterSelected].transform.GetChild(7).GetChild(1).gameObject.SetActive(true);
        characterPanels[characterSelected].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Attack: " + playerScript.party[characterSelected].Attack;

        CloseWeaponWindow();
    }


    //Equip shield button method (Same implementation as EquipWeapon)
    public void EquipShield(GameObject shieldName)
    {
        int shieldIndex = 0;
        for (int i = 0; i < inventoryScript.s_list.Count; i++)
        {
            if  (inventoryScript.s_list[i].name == shieldName.GetComponent<TextMeshProUGUI>().text)
            {
                shieldIndex = i;
                characterPanels[characterSelected].transform.GetChild(8).GetComponent<Image>().sprite = inventoryScript.s_list[i].image;
            }
        }

        inventoryScript.equip_shield(shieldIndex, playerScript.party[characterSelected]);
        characterPanels[characterSelected].transform.GetChild(8).GetChild(1).gameObject.SetActive(true);
        characterPanels[characterSelected].transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Defense: " + playerScript.party[characterSelected].Defense;
        
        CloseShieldWindow();
    }

    public void OpenInventory()
    {
        this.gameObject.SetActive(true);
        inventoryControls.SetActive(false);
    }

    public void CloseInventory()
    {
        this.gameObject.SetActive(false);
        inventoryControls.SetActive(true);
    }

    public void CloseWeaponWindow()
    {
        //Close window tab
        weaponWindow.SetActive(false);
        
        //Destroy the list (will get repopulated when window is opened again)
        foreach(Transform child in weaponContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CloseShieldWindow()
    {
        //Close window tab
        shieldWindow.SetActive(false);
        
        //Destroy the list (will get repopulated when window is opened again)
        foreach(Transform child in shieldContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UnequipWeapon(int characterNumber)
    {
        inventoryScript.unequip_weapon(playerScript.party[characterNumber]);
        characterPanels[characterNumber].transform.GetChild(7).GetComponent<Image>().sprite = null;
        characterPanels[characterSelected].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Attack: " + playerScript.party[characterSelected].Attack;

        characterPanels[characterNumber].transform.GetChild(7).GetChild(1).gameObject.SetActive(false);
    }

    public void UnequipShield(int characterNumber)
    {
        inventoryScript.unequip_shield(playerScript.party[characterNumber]);
        characterPanels[characterNumber].transform.GetChild(8).GetComponent<Image>().sprite = null;
        characterPanels[characterSelected].transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Defense: " + playerScript.party[characterSelected].Defense;

        characterPanels[characterNumber].transform.GetChild(8).GetChild(1).gameObject.SetActive(false);
    }
}

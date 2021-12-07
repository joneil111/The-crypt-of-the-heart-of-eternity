using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponManager : MonoBehaviour
{
    


    [SerializeField]
    private WeapoHandler[] Weapons;

    private int current_Weapon_Index;
    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
        Weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            TurnOnSelectedWeapon(0);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            TurnOnSelectedWeapon(2);
        }
    }

    void TurnOnSelectedWeapon(int index)
    {
        /*
         stop repeating draw animation on same weapon draw
         if(index == current_Weapon_Index)
            return;
         
         */

        Weapons[current_Weapon_Index].gameObject.SetActive(false);
        Weapons[index].gameObject.SetActive(true);

        current_Weapon_Index = index;
    }

    public WeapoHandler GetCurrentSelectedWeapon()
    {
        return Weapons[current_Weapon_Index];
    }
}

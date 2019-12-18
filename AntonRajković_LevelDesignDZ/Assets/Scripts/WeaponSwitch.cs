using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    Gun GunScript;
    bool CanSwitch;

    // Start is called before the first frame update
    void Start()
    {
        selectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(GunScript != null)
        {
            CanSwitch = !GunScript.CheckIfReloading();
        }

        int previousWeapon = selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && CanSwitch)
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f && CanSwitch)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if(previousWeapon != selectedWeapon)
        {
            selectWeapon();
        }
    }

    void selectWeapon()
    {
        int i = 0;
        foreach(Transform weapons in transform)
        {
            if(i == selectedWeapon)
            {
                weapons.gameObject.SetActive(true);
                GunScript = weapons.gameObject.GetComponent<Gun>();
            }
            else
            {
                weapons.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

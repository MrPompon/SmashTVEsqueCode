using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Flash : MonoBehaviour
{
    public Weapon weapon;
    public GameObject gFlashObject;
    public float upTime = 0.4f;
    private void Start()
    {
        if(weapon!=null)
            weapon.OnFire += RunFlash;
    }
    private void OnDestroy()
    {
        if (weapon != null)
            weapon.OnFire -= RunFlash;
    }
    void RunFlash()
    {
        StartCoroutine(Flash(upTime));
    }
    IEnumerator Flash(float upTime)
    {
        EnableFlash(true);
        yield return new WaitForSeconds(upTime);
        EnableFlash(false);
    }
    private void EnableFlash(bool value)
    {

        if (gFlashObject != null)
        {
            if (gFlashObject.activeInHierarchy != value)
            {
                gFlashObject.SetActive(value);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public Player_Controller player;

    public CanvasGroup canvasGroup;
    public Image healthBar;
    public TextMeshProUGUI ammoText;

    public InAudioNode badHealthWarning;
    public InAudioNode mediumHealthWarning;
    [Range(0f,1f)]public float healthStatusGood, healthStatusMedium, healthStatusBad;
    public Color color_healthStatusGood, color_healthStatusMedium, color_healthStatusBad;

    private Player_Weapon playerWeapon;
    private float displayCooldown=2f;
    private float displayProgress = 0;
    private void Awake()
    {
        playerWeapon = player.gun as Player_Weapon;
        player.OnAnyInput += HideDisplay;
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            HideDisplay();
            return;
        }
        displayProgress += Time.deltaTime;
        if(displayProgress >= displayCooldown)
        {
            ShowDisplay();
        }
    }
    void HideDisplay()
    {
        if (badHealthWarning != null)
        {
            InAudio.Stop(this.gameObject, badHealthWarning);
        }
        if (mediumHealthWarning != null)
        {
            InAudio.Stop(this.gameObject, mediumHealthWarning);
        }
        displayProgress = 0;
        canvasGroup.alpha = 0;
    }
    void ShowDisplay()
    {
        UpdateUIElements();
        canvasGroup.alpha = 1;
    }
    void UpdateUIElements()
    {
        UpdateAmmoDisplay();
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        float healthPercent = ((float)player.statHandler.CurrentHP / (float)player.statHandler.baseStats.HP);
        healthBar.fillAmount = healthPercent;

        if (healthPercent <= healthStatusBad)
        {
            if (badHealthWarning != null)
            {
                if (mediumHealthWarning != null)
                {
                    InAudio.Stop(this.gameObject, mediumHealthWarning);
                }
                InAudio.Play(this.gameObject, badHealthWarning);
            }
            healthBar.color = color_healthStatusBad;
        }
        else if(healthPercent<= healthStatusMedium)
        {
            if (mediumHealthWarning != null)
            {
                if (badHealthWarning != null)
                {
                    InAudio.Stop(this.gameObject, badHealthWarning);
                }
                InAudio.Play(this.gameObject, mediumHealthWarning);
            }
            healthBar.color = color_healthStatusMedium;
        }
        else
        {
            healthBar.color = color_healthStatusGood;
        }
    }
    void UpdateAmmoDisplay()
    {
        if (playerWeapon == null)
        {
            return;
        }
        int current=playerWeapon.CurrentShots;
        int max = playerWeapon.ClipSize;
        ammoText.text = current.ToString() + " / " + max.ToString();
    }
}

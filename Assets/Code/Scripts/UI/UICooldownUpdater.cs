using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooldownUpdater : MonoBehaviour
{
    [SerializeField]
    private AbilityHandler handler;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Image mask;
    [SerializeField]
    private TMP_Text cooldownMesh;
    [SerializeField]
    private TMP_Text manaCostMesh;

    public void SetHandler(AbilityHandler _handler)
    {
        handler = _handler;
        manaCostMesh.text = handler.GetAbilityData().manaCost.ToString();
        icon.sprite = handler.GetAbilityData().icon;
    }

    void Update()
    {
        if (handler.isCoolingDown)
        {
            float cooldown = handler.GetAbilityData().cooldownDuration;
            mask.fillAmount = 1 - (handler.currentTime / cooldown);
            cooldownMesh.text = (cooldown - handler.currentTime).ToString("0.##");
        }
        else
        {
            cooldownMesh.text = "";
            mask.fillAmount = 0;
        }
    }
}

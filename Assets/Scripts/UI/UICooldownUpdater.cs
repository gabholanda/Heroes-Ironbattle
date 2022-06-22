using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooldownUpdater : MonoBehaviour
{
    [SerializeField]
    private AbilityHandler handler;
    [SerializeField]
    private Image mask;
    [SerializeField]
    private TMP_Text textMesh;

    public void SetHandler(AbilityHandler _handler)
    {
        handler = _handler;
    }

    void Update()
    {
        if (handler.isCoolingDown)
        {
            float cooldown = handler.GetAbilityData().cooldownDuration;
            mask.fillAmount = 1 - (handler.currentTime / cooldown);
            textMesh.text = (cooldown - handler.currentTime).ToString("0.##");
        }
        else
        {
            textMesh.text = "";
            mask.fillAmount = 0;
        }
    }
}

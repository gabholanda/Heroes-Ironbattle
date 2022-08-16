﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsTabManager : MonoBehaviour
{
    private CharacterStats stats;

    public Image container;
    public TextMeshProUGUI resources;
    public TextMeshProUGUI combat;
    public TextMeshProUGUI defenses;
    public TextMeshProUGUI affinities;
    public TextMeshProUGUI resistances;

    private GameEvent OnHurt;
    private GameEventListener listener;
    public void Initialize()
    {
        GetPlayer();
        UpdateTab();
    }

    public void GetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterStateMachine sm = player.GetComponent<CharacterStateMachine>();
        listener = gameObject.AddComponent<GameEventListener>();
        stats = sm.stats;
        OnHurt = sm.eventManager.events["Hurt"];
        listener.Event = OnHurt;
        listener.Response.AddListener(UpdateResources);
    }

    public void ShowTab()
    {
        UpdateResources();
        container.gameObject.SetActive(true);
    }

    public void HideTab()
    {
        container.gameObject.SetActive(false);
    }

    public void UpdateTab()
    {
        combat.text = stats.combatStats.ToString();
        defenses.text = stats.defensesResistances.ToString();
        string affinityStr = "<size=75%>Elemental Affinities\n<size=100%>";
        string resistanceStr = "<size=75%>Elemental Resistances\n<size=100%>";
        foreach (var kv in stats.elements)
        {
            affinityStr += "<sprite name=" + stats.elements[kv.Key].affinityIcon + "> " + stats.elements[kv.Key].affinity * 100 + "%\n";
            resistanceStr += "<sprite name=" + stats.elements[kv.Key].resistanceIcon + "> " + stats.elements[kv.Key].resistance * 100 + "%\n";
        }
        affinities.text = affinityStr;
        resistances.text = resistanceStr;
    }

    public void UpdateResources()
    {
        resources.text = stats.resources.ToString();
    }
}
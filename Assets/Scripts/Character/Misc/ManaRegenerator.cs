using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegenerator : MonoBehaviour
{
    private CharacterStats stats;
    private SlidingBar manaBar;
    // Start is called before the first frame update
    public ManaRegenerator SetStats(CharacterStats _stats)
    {
        stats = _stats;
        return this;
    }

    public ManaRegenerator SetManaBar(SlidingBar _manaBar)
    {
        manaBar = _manaBar;
        return this;
    }

    public void StartRegeneration()
    {
        StartCoroutine(RegenerateMana());
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (stats.CurrentMana < stats.MaxMana)
            {
                stats.CurrentMana += stats.RegenRate;
            }
            else if (stats.CurrentMana > stats.MaxMana)
            {
                stats.CurrentMana = stats.MaxMana;
            }
            if (stats.MaxMana != stats.CurrentMana)
                manaBar.UpdateBar(stats.CurrentMana / stats.MaxMana);

        }
    }
}

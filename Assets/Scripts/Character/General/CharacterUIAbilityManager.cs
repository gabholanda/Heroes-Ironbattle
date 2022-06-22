using UnityEngine;

public class CharacterUIAbilityManager : MonoBehaviour
{
    private AbilityHandler[] handlers;
    [SerializeField]
    private UICooldownUpdater QAbilityUI;
    [SerializeField]
    private UICooldownUpdater EAbilityUI;
    [SerializeField]
    private UICooldownUpdater RAbilityUI;

    private void Awake()
    {
        QAbilityUI = GameObject.FindGameObjectWithTag("QSkill").GetComponent<UICooldownUpdater>();
        EAbilityUI = GameObject.FindGameObjectWithTag("ESkill").GetComponent<UICooldownUpdater>();
        RAbilityUI = GameObject.FindGameObjectWithTag("RSkill").GetComponent<UICooldownUpdater>();
    }

    public CharacterUIAbilityManager SetAbilityHandlers(AbilityHandler[] _handlers)
    {
        handlers = _handlers;
        return this;
    }

    public CharacterUIAbilityManager SetHandlers()
    {
        QAbilityUI.SetHandler(handlers[0]);
        EAbilityUI.SetHandler(handlers[1]);
        RAbilityUI.SetHandler(handlers[2]);
        return this;
    }
}

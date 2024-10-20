using UnityEngine;

public class MonsterEvents : MonoBehaviour
{
    public delegate void MonsterDeathDelegate(GameObject g);
    public event MonsterDeathDelegate MonsterDeathEvent;
    private void OnDestroy()
    {
        if (MonsterDeathEvent != null)
            MonsterDeathEvent(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyAfter;
    private float current = 0;
    // Start is called before the first frame update
    public void StartSelfDestroy()
    {
        StartCoroutine(Destroy());
    }

    public IEnumerator Destroy()
    {
        while (current < destroyAfter)
        {
            current += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}

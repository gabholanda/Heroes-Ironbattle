using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameEvent OnTryAgain;

    public void EnableChild()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void DisableChild()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void TryAgain()
    {
        OnTryAgain?.Raise();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField]
    private GameObject textMeshObject;
    [SerializeField]
    private float speed;
    private TextMeshPro textMesh;
    private float direction;
    private bool isUp;

    public void SetDamageText(float value)
    {
        textMesh = textMeshObject.GetComponent<TextMeshPro>();
        direction = Random.Range(-1, 1) == 0 ? 1 : -1;
        isUp = true;
        StartCoroutine(SwitchToDown());
        textMesh.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (isUp)
        {
            GoUp(pos);
        }
        else
        {
            GoDown(pos);
        }
    }

    private void GoUp(Vector3 pos)
    {
        float newX = speed * Time.deltaTime * 0.2f * direction;
        float newY = speed * Time.deltaTime * 0.4f;
        transform.position += new Vector3(newX, newY, pos.z);
        textMesh.fontSize += 0.1f;
    }

    private void GoDown(Vector3 pos)
    {
        float newX = speed * Time.deltaTime * 0.2f * direction;
        float newY = speed * Time.deltaTime * 0.5f;
        transform.position += new Vector3(newX, -newY, pos.z);
        textMesh.fontSize -= 0.8f;

        if (textMesh.fontSize <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SwitchToDown()
    {
        yield return new WaitForSeconds(0.3f);
        isUp = false;
    }

}

using System.Collections;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField]
    private GameObject textMeshObject;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Color vertexColor;
    [SerializeField]
    [ColorUsage(true, true)]
    private Color outlineColor;
    [SerializeField]
    [ColorUsage(true, true)]
    private Color glowColor;
    private TextMeshPro textMesh;
    private float direction;
    private bool isUp;

    public DamagePopUp SetColor(Color _color)
    {
        vertexColor = _color;
        return this;
    }

    public DamagePopUp SetOutlineColor(Color _color)
    {
        outlineColor = _color;
        return this;
    }

    public DamagePopUp SetGlowColor(Color _color)
    {
        glowColor = _color;
        return this;
    }

    public void SetDamageText(float value)
    {
        textMesh = textMeshObject.GetComponent<TextMeshPro>();
        direction = Random.Range(-1, 1) == 0 ? 1 : -1;
        isUp = true;
        StartCoroutine(SwitchToDown());
        textMesh.color = vertexColor;
        textMesh.fontMaterial.SetColor("_OutlineColor", outlineColor);
        textMesh.fontMaterial.SetColor("_GlowColor", glowColor);
        textMesh.text = value.ToString();
    }

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

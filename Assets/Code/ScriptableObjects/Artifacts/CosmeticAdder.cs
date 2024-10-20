using UnityEngine;

[CreateAssetMenu(fileName = "Cosmetic Adder", menuName = "ScriptableObjects/New Cosmetic Adder")]
public class CosmeticAdder : ScriptableObject
{
    public GameObject cosmetic;
    public Vector3 offsets;
    public void Add(GameObject target)
    {
        GameObject obj = Instantiate(cosmetic, target.transform);
        obj.transform.position += offsets;
    }
}

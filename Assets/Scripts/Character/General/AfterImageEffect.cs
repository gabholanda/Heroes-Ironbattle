using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public float intervalPerImage;
    private float currentTime = 0f;
    public float destroyAfterSeconds;
    public Color32 color;
    public SpriteRenderer spriteRenderer;
    public GameObject prefab;
    private CharacterMovement characterMove;

    private void Start()
    {
        StartCoroutine(GetCharacterMovementComponent());
    }

    void Update()
    {
        if (characterMove.isDashing)
            currentTime += Time.deltaTime;
        if (currentTime > intervalPerImage)
        {
            GameObject obj = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            obj.transform.localScale = transform.localScale;
            SpriteRenderer sprRend = obj.GetComponent<SpriteRenderer>();
            sprRend.color = color;
            sprRend.sprite = spriteRenderer.sprite;
            SelfDestroy component = obj.AddComponent<SelfDestroy>();
            component.destroyAfter = destroyAfterSeconds;
            component.StartSelfDestroy();
            currentTime = 0f;
        }
    }

    private IEnumerator GetCharacterMovementComponent()
    {
        yield return new WaitForFixedUpdate();
        characterMove = GetComponent<CharacterMovement>();
    }
}

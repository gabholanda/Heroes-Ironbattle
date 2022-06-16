using UnityEngine;

public class TeleporterManager : MonoBehaviour, Interactable
{
    private MapManager mapManager;
    private CharacterInteractor interactor;

    void Awake()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
    }

    public void Interact(GameObject g)
    {
        interactor.OnInteract -= Interact;
        mapManager.StartGeneratingMap();
        Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        interactor = collision.gameObject.GetComponent<CharacterInteractor>();
        if (interactor != null)
            interactor.OnInteract += Interact;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactor = collision.gameObject.GetComponent<CharacterInteractor>();
        if (interactor != null)
            interactor.OnInteract -= Interact;
    }
}

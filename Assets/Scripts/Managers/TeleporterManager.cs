using UnityEngine;

public class TeleporterManager : MonoBehaviour, Interactable
{
    private MapManager mapManager;
    private CharacterInteractor interactor;
    private bool hasSelectedArtifact;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private GameObject artifactCanvas;

    void Awake()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        hasSelectedArtifact = false;

        GameObject canvas = Instantiate(artifactCanvas);
        canvas.SetActive(true);
    }

    public void OnHasSelectedArtifact()
    {
        hasSelectedArtifact = true;
    }

    public void Interact(GameObject g)
    {
        if (hasSelectedArtifact)
        {
            interactor.OnInteract -= Interact;
            mapManager.StartGeneratingMap();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            source.Play();
        }
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

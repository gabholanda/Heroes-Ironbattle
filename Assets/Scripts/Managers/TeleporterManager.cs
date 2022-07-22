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

    void Start()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        if (artifactCanvas is null)
            artifactCanvas = GameObject.FindGameObjectWithTag("ArtifactCanvas");
    }

    private void OnEnable()
    {
        hasSelectedArtifact = false;
        artifactCanvas?.SetActive(true);
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
            transform.parent.gameObject.SetActive(false);
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

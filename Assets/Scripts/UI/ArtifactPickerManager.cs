using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;

public class ArtifactPickerManager : MonoBehaviour
{
    [Header("Item Variables")]
    public ArtifactInventory allArtifacts;
    public List<Rarity> rarityList = new List<Rarity>();

    [Header("UI Variables")]
    public Button firstArtifactButton;
    public TextMeshProUGUI firstArtifactText;
    public Image firstArtifactImage;
    public TextMeshProUGUI firstArtifactDescription;


    public Button secondArtifactButton;
    public TextMeshProUGUI secondArtifactText;
    public Image secondArtifactImage;
    public TextMeshProUGUI secondArtifactDescription;

    public Button thirdArtifactButton;
    public TextMeshProUGUI thirdArtifactText;
    public Image thirdArtifactImage;
    public TextMeshProUGUI thirdArtifactDescription;

    [Header("Event")]
    [SerializeField]
    private GameEvent OnSelectEvent;


    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartSelection();
    }

    public void StartSelection()
    {
        List<Artifact> filteredArtifacts = FilterArtifactsByRarity();
        PickChoices(filteredArtifacts);
        firstArtifactButton.interactable = true;
        secondArtifactButton.interactable = true;
        thirdArtifactButton.interactable = true;
        DisplayChoices();
    }

    private Rarity ChooseRandomRarity()
    {
        return rarityList[Random.Range(0, rarityList.Count)];
    }

    private List<Artifact> FilterArtifactsByRarity()
    {
        List<Artifact> filteredArtifacts = new List<Artifact>();
        Rarity chosenRarity = ChooseRandomRarity();


        allArtifacts.Items.ForEach((inventoryItem) =>
        {
            if (inventoryItem.Item.rarity == chosenRarity)
            {
                filteredArtifacts.Add(inventoryItem.Item);
            }
        });
        return filteredArtifacts;
    }

    public Artifact PickRandomArtifact(List<Artifact> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    private void PickChoices(List<Artifact> filteredArtifacts)
    {
        Artifact first = PickRandomArtifact(filteredArtifacts);
        //filteredArtifacts.Remove(first);
        Artifact second = PickRandomArtifact(filteredArtifacts);
        //filteredArtifacts.Remove(second);
        Artifact third = PickRandomArtifact(filteredArtifacts);

        firstArtifactText.text = first.name;
        secondArtifactText.text = second.name;
        thirdArtifactText.text = third.name;

        firstArtifactImage.sprite = first.icon;
        secondArtifactImage.sprite = second.icon;
        thirdArtifactImage.sprite = third.icon;

        firstArtifactDescription.text = first.description;
        secondArtifactDescription.text = second.description;
        thirdArtifactDescription.text = third.description;

        firstArtifactButton.onClick.AddListener(delegate { SetChoice(first); });
        secondArtifactButton.onClick.AddListener(delegate { SetChoice(second); });
        thirdArtifactButton.onClick.AddListener(delegate { SetChoice(third); });
    }

    private void DisplayChoices()
    {
        gameObject.SetActive(true);
    }

    private void SetChoice(Artifact artifact)
    {
        firstArtifactButton.interactable = false;
        secondArtifactButton.interactable = false;
        thirdArtifactButton.interactable = false;
        firstArtifactButton.onClick.RemoveAllListeners();
        secondArtifactButton.onClick.RemoveAllListeners();
        thirdArtifactButton.onClick.RemoveAllListeners();
        player.GetComponent<PlayerStateMachine>().inventory.Add(new ArtifactInventoryItem(artifact));
        OnSelectEvent.Raise();
        Destroy(gameObject);
    }
}

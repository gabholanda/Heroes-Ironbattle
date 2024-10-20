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
    public ArtifactInventory globalRaritiesInventory;

    [Header("UI Variables")]
    public Button firstArtifactButton;
    public Image firstArtifactBackground;
    public TextMeshProUGUI firstArtifactText;
    public Image firstArtifactImage;
    public TextMeshProUGUI firstArtifactDescription;


    public Button secondArtifactButton;
    public Image secondArtifactBackground;
    public TextMeshProUGUI secondArtifactText;
    public Image secondArtifactImage;
    public TextMeshProUGUI secondArtifactDescription;

    public Button thirdArtifactButton;
    public Image thirdArtifactBackground;
    public TextMeshProUGUI thirdArtifactText;
    public Image thirdArtifactImage;
    public TextMeshProUGUI thirdArtifactDescription;

    private Rarity chosenRarity;

    [Header("Event")]
    [SerializeField]
    private GameEvent OnSelectEvent;


    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartSelection();
    }

    public void StartSelection()
    {
        List<Artifact> filteredArtifacts = FilterArtifactsByRarity();
        PickChoices(filteredArtifacts);
        EnableButtons();
        DisplayChoices();
    }

    private Rarity ChooseRandomRarity()
    {
        return rarityList[Random.Range(0, rarityList.Count)];
    }

    private List<Artifact> FilterArtifactsByRarity()
    {
        List<Artifact> filteredArtifacts = new List<Artifact>();
        chosenRarity = ChooseRandomRarity();
        globalRaritiesInventory.applyableRarities.Add(chosenRarity);
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

        firstArtifactBackground.color = chosenRarity.color;
        secondArtifactBackground.color = chosenRarity.color;
        thirdArtifactBackground.color = chosenRarity.color;

        firstArtifactButton.onClick.AddListener(delegate { SetChoice(first); });
        secondArtifactButton.onClick.AddListener(delegate { SetChoice(second); });
        thirdArtifactButton.onClick.AddListener(delegate { SetChoice(third); });
    }

    private void DisplayChoices()
    {
        gameObject.SetActive(true);
    }

    private void EnableButtons()
    {
        firstArtifactButton.interactable = true;
        secondArtifactButton.interactable = true;
        thirdArtifactButton.interactable = true;
    }

    private void SetChoice(Artifact artifact)
    {

        player.GetComponent<InventoryManager>().inventory.Add(new ArtifactInventoryItem(artifact));
        DeactivateCanvas();
        OnSelectEvent.Raise();
    }

    public void DeactivateCanvas()
    {
        firstArtifactButton.interactable = false;
        secondArtifactButton.interactable = false;
        thirdArtifactButton.interactable = false;
        firstArtifactButton.onClick.RemoveAllListeners();
        secondArtifactButton.onClick.RemoveAllListeners();
        thirdArtifactButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtifactPickerManager : MonoBehaviour
{
    public ArtifactInventory commonArtifacts;
    public ArtifactInventory rareArtifacts;
    public ArtifactInventory uniqueArtifacts;
    public ArtifactInventory legendaryArtifacts;

    public Button firstArtifactButton;
    public TextMeshProUGUI firstArtifactText;
    public Image firstArtifactImage;


    public Button secondArtifactButton;
    public TextMeshProUGUI secondArtifactText;
    public Image secondArtifactImage;


    public Button thirdArtifactButton;
    public TextMeshProUGUI thirdArtifactText;
    public Image thirdArtifactImage;

    private void Awake()
    {
        OnDisplayChoices();
    }

    public void OnDisplayChoices()
    {
        Debug.Log(PickCommonRandom());
        Artifact first = PickCommonRandom();
        Artifact second = PickCommonRandom();
        Artifact third = PickCommonRandom();
        firstArtifactText.text = first.name;
        secondArtifactText.text = second.name;
        thirdArtifactText.text = third.name;

        firstArtifactImage.sprite = first.icon;
        secondArtifactImage.sprite = second.icon;
        thirdArtifactImage.sprite = third.icon;
    }

    public Artifact PickCommonRandom()
    {
        return commonArtifacts.Items[Random.Range(0, commonArtifacts.Items.Count)];
    }


}

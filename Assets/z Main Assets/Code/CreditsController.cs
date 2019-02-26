using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public TextAsset creditsFile;

    private void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = creditsFile.text;
    }
}
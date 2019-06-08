using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] public Sprite[] shipSprites;
    [SerializeField] public Image shipImage;

    private int shipI;
    // Start is called before the first frame update
    void Start()
    {
        shipI = PlayerPrefs.GetInt("ShipShape");
        shipImage.sprite = shipSprites[shipI];
    }

    public void OnNextButton() {
        shipImage.sprite = shipSprites[Mathf.Abs((shipI++) % 3)];
    }

    public void OnPrevButton() {
        shipImage.sprite = shipSprites[Mathf.Abs((shipI--) % 3)];
    }

    public void OnExitButton() {
        SceneManager.LoadScene("Main Menu");
    }
}

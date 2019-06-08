using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerScript : MonoBehaviour
{
    public GameObject panel;

    public void ShowWinner() {
        panel.SetActive(true);
    }

}

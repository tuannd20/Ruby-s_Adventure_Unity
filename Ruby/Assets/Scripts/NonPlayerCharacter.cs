using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayBoxTime = 4.0f;
    public GameObject dialogBox;

    float timerDisplay= 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0) {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0) {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog() {
        timerDisplay = displayBoxTime;
        dialogBox.SetActive(true);
    }
}

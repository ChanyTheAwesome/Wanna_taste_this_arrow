using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private Button thirdButton;
    [SerializeField] private Image firstPage;
    [SerializeField] private Image secondPage;
    // Start is called before the first frame update
    void Start()
    {
        firstPage.gameObject.SetActive(false);
        secondPage.gameObject.SetActive(false);
        secondButton.gameObject.SetActive(false);
        thirdButton.gameObject.SetActive(false);

        firstButton.onClick.AddListener(FirstPagePrint);
        secondButton.onClick.AddListener(SecondPagePrint);
        thirdButton.onClick.AddListener(TutorialExit);
    }

    public void FirstPagePrint()
    {
        firstPage.gameObject.SetActive(true);
        firstButton.gameObject.SetActive(false);
        secondButton.gameObject.SetActive(true);
    }

    public void SecondPagePrint()
    {
        firstPage.gameObject.SetActive(false);
        secondPage.gameObject.SetActive(true);
        firstButton.gameObject.SetActive(false);
        secondButton.gameObject.SetActive(false);
        thirdButton.gameObject.SetActive(true);
    }

    public void TutorialExit()
    {
        firstPage.gameObject.SetActive(false);
        secondPage.gameObject.SetActive(false);
        firstButton.gameObject.SetActive(true);
        secondButton.gameObject.SetActive(false);
        thirdButton.gameObject.SetActive(false);
    }
}

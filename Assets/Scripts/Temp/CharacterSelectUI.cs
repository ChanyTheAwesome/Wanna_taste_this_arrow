using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    GameManager gameManager = GameManager.Instance;

    [SerializeField] private Button firstIndex;
    [SerializeField] private Button secondIndex;
    [SerializeField] private Button thirdIndex;
    [SerializeField] private Button fourthIndex;

    // Start is called before the first frame update
    void Start()
    {
        firstIndex.onClick.AddListener(OnClickFirstButton);
        secondIndex.onClick.AddListener(OnClickSecondButton);
        thirdIndex.onClick.AddListener(OnClickThirdButton);
        fourthIndex.onClick.AddListener(OnClickFourthButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null) Debug.Log("gm null");
    }

    public void OnClickFirstButton()
    {
        gameObject.SetActive(false);
        gameManager.SelectedIndex = 0;
        gameManager.SetCharacter();
        
    }
    public void OnClickSecondButton()
    {

        gameObject.SetActive(false);
        gameManager.SelectedIndex = 1;
    }
    public void OnClickThirdButton()
    {

        gameObject.SetActive(false);
        gameManager.SelectedIndex = 2;
    }
    public void OnClickFourthButton()
    {

        gameObject.SetActive(false);
        gameManager.SelectedIndex = 3;
    }
}

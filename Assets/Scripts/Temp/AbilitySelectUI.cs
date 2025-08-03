using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelectUI : MonoBehaviour
{
    [SerializeField] private GameObject selectPrefab;
    [SerializeField] private Transform selectPos;

    private PlayerController playerController;

    public void ShowSelect(List<AbilityData> abilities, PlayerController controller)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        playerController = controller;

        foreach (AbilityData ability in abilities)
        {
            GameObject select = Instantiate(selectPrefab, selectPos);
            AbilityCard card = select.GetComponent<AbilityCard>();
            card.SetInfo(ability, playerController, this);
        }
    }

    public void Hide()
    {
        foreach (Transform child in selectPos)
        {
            Destroy(child.gameObject);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}

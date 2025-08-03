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

        Vector3 basePos = selectPos.position;
        basePos.x += 200;
        for (int i = 0; i < abilities.Count; i++)
        {
            Vector3 newPos = basePos;
            newPos.x -= 200f * i;
            GameObject select = Instantiate(selectPrefab, selectPos);
            select.transform.position = newPos;
            AbilityCard card = select.GetComponent<AbilityCard>();
            card.SetInfo(abilities[i], playerController, this);
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

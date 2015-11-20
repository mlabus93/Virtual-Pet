﻿using UnityEngine;
using System.Collections;

public class LetsEat : MonoBehaviour{
    GameObject player;
    public GameObject uiController;

    bool playerInRange;

    void OnMouseDown()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MoveToAction>().GoToFoodTable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            uiController.GetComponent<UIController>().drinkBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().turkeyBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().chickenBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().beefBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().ribBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().fishBtn.gameObject.SetActive(true);
            uiController.GetComponent<UIController>().treatBtn.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            uiController.GetComponent<UIController>().drinkBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().turkeyBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().chickenBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().beefBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().ribBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().fishBtn.gameObject.SetActive(false);
            uiController.GetComponent<UIController>().treatBtn.gameObject.SetActive(false);
        }
    }
}


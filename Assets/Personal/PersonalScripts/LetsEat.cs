using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace PersonalScripts
{
    public class LetsEat : MonoBehaviour
    {
        GameObject player;
        GameObject canvasClone;

        void OnMouseDown()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (!EventSystem.current.IsPointerOverGameObject() && player != null)
            {
                player.GetComponent<MoveToAction>().GoToFoodTable();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                canvasClone = GameObject.Find("CanvasMain(Clone)");
                canvasClone.GetComponent<UIController>().drinkBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().turkeyBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().chickenBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().beefBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().ribBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().fishBtn.gameObject.SetActive(true);
                canvasClone.GetComponent<UIController>().treatBtn.gameObject.SetActive(true);
                player.GetComponent<MoveToAction>().inTarget = true;
                player.GetComponent<MoveToAction>().StopPlayer();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                canvasClone = GameObject.Find("CanvasMain(Clone)");
                canvasClone.GetComponent<UIController>().drinkBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().turkeyBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().chickenBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().beefBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().ribBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().fishBtn.gameObject.SetActive(false);
                canvasClone.GetComponent<UIController>().treatBtn.gameObject.SetActive(false);
                player.GetComponent<MoveToAction>().inTarget = false;
            }
        }
    }

}

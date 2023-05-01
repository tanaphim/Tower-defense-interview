using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace TowerDefense
{
    public class FloorObject : MonoBehaviour, IPointerClickHandler
    {
        private Turrent turrent;
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            TurrentSelection.I.ShowPanel(TurrentSelected);
        }

        public void TurrentSelected(Turrent newTurrent)
        {
            if (turrent) Destroy(turrent.gameObject);
            turrent = Instantiate<Turrent>(newTurrent);
            turrent.transform.position = transform.position;
            TurrentSelection.I.HidePanel();
        }
    }
}
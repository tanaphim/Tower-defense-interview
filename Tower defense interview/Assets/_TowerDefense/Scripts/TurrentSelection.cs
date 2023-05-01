using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace TowerDefense
{
    public class TurrentSelection : MonoSingleton<TurrentSelection>
    {
        [SerializeField] private Button[] buttons;
        private CanvasGroup canvasGroup;
        private Action<Turrent> OnSelected;

        public override void Init()
        {
            base.Init();
            canvasGroup = GetComponent<CanvasGroup>();
            for (int i = 0; i < buttons.Length; i++)
            {
                int id = i;
                buttons[id].onClick.AddListener(() => SelectTurrent(id));
            }
        }

        private void Start()
        {
            HidePanel();
        }

        public void ShowPanel(Action<Turrent> selectedCallback)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            OnSelected = selectedCallback;
        }

        public void HidePanel()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        private void SelectTurrent(int id)
        {
            OnSelected?.Invoke(TurrentManager.I.GetTurrent(id));
        }
    }
}
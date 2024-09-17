using System;
using System.Collections.Generic;
using UnityEngine;

namespace CiFarm.Scripts.UI.View.GameViewComponent
{
    public class ToolManager : MonoBehaviour
    {
        [SerializeField] private List<ToolItem> toolItems;

        
        private void Start()
        {
        }

        public void InitItems()
        {
        }

        public void OnClickItem(int index)
        {
            for (int i = 0; i < toolItems.Count; i++)
            {
                toolItems[i].SetActive(i == index);
            }
        }
        
    }

    public enum ToolName
    {
        WaterCan,
        Scythe,
        Pesticide,
        Herbicide,
    }
}
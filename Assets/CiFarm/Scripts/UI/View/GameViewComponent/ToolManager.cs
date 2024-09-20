using System;
using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using UnityEngine;

namespace CiFarm.Scripts.UI.View.GameViewComponent
{
    public class ToolManager : MonoBehaviour
    {
        [SerializeField] private List<ToolItem> toolItems;

        [SerializeField] public List<ToolData> toolDatas;

        private int _inventoryPage = 0;
        private int _currentSelectIndex;

        public ToolData CurrentTool
        {
            get
            {
                DLogger.Log("Index Tool: " + _inventoryPage * 4 + _currentSelectIndex, "ToolManager");

                var tool = toolDatas[_inventoryPage * 4 + _currentSelectIndex];
                DLogger.Log("Current Tool: " + tool.toolType, "ToolManager");
                return tool;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < toolItems.Count; i++)
            {
                var index = i;
                toolItems[i].InitAction(() => { OnClickItem(index); });
            }
        }

        private void Start()
        {
            toolItems[0].SetSelect(true);
        }

        public void LoadTool()
        {
            for (int i = 0; i < toolItems.Count; i++)
            {
                toolItems[0].InitIcon(toolDatas[_inventoryPage * toolItems.Count + i].toolIc);
            }
        }

        public void OnClickItem(int index)
        {
            for (int i = 0; i < toolItems.Count; i++)
            {
                toolItems[i].SetSelect(i == index);
            }

            _currentSelectIndex = index;
        }

        public void OnClickLeft()
        {
            _inventoryPage = _inventoryPage == 0 ? 0 : _inventoryPage--;
            LoadTool();
        }

        public void OnClickRight()
        {
            var maxPageFloat = toolDatas.Count / 4f;
            var maxPage      = (int)Mathf.Ceil(maxPageFloat);
            _inventoryPage = _inventoryPage == maxPage ? maxPage : _inventoryPage++;
            LoadTool();
        }
    }

    [Serializable]
    public class ToolData
    {
        public string   key;
        public ToolType toolType;
        public Sprite   toolIc;
    }

    public enum ToolType
    {
        WaterCan,
        Steal,
        Scythe,
        Pesticide,
        Herbicide,
        Fertilizer,
        Moving,
        PlacingItem
    }
}
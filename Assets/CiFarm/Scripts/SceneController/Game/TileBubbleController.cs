using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileBubbleController : ManualSingletonMono<TileBubbleController>
    {
        private HashSet<string> showingId;

        public override void Awake()
        {
            base.Awake();
            showingId = new HashSet<string>();
        }

        public bool CheckBubble(string id)
        {
            return showingId.Contains(id);
        }

        public void OnBubbleAppear(string id)
        {
            showingId.Add(id);
        }

        public void OnBubbleDisappear(string id)
        {
            showingId.Remove(id);
        }
    }
}
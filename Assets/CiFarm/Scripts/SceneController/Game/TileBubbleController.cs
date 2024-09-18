using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileBubbleController : ManualSingletonMono<TileBubbleController>
    {
        private HashSet<string> showingId;
        private Dictionary<string, DirtBubble>   showingBubble;

        public override void Awake()
        {
            base.Awake();
            showingBubble = new Dictionary<string, DirtBubble>();
        }

        public bool CheckBubble(string id)
        {
            return showingBubble.ContainsKey(id);
        }
        
        public void OnBubbleAppear(string id, DirtBubble bubble)
        {
            showingBubble.TryAdd(id, bubble);
        }

        public void OnBubbleDisappear(string id)
        {
            if (showingBubble.ContainsKey(id))
            {
                showingBubble.Remove(id);
            }
        }

        public void HideBubble(string id)
        {
            if (showingBubble.TryGetValue(id, out DirtBubble bubble))
            {
                bubble.OffBubble();
                showingBubble.Remove(id);
            }
        }
    }
}
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileBubbleController : ManualSingletonMono<TileBubbleController>
    {
        private Dictionary<string, DirtBubble>   _showingBubble;

        public override void Awake()
        {
            base.Awake();
            _showingBubble = new Dictionary<string, DirtBubble>();
        }

        public bool CheckBubble(string id)
        {
            return _showingBubble.ContainsKey(id);
        }
        
        public void OnBubbleAppear(string id, DirtBubble bubble)
        {
            _showingBubble.TryAdd(id, bubble);
        }

        public void OnBubbleDisappear(string id)
        {
            if (_showingBubble.ContainsKey(id))
            {
                _showingBubble.Remove(id);
            }
        }

        public void HideBubble(string id)
        {
            if (_showingBubble.TryGetValue(id, out DirtBubble bubble))
            {
                bubble.OffBubble();
                _showingBubble.Remove(id);
            }
        }
        public void ClearAllBubble()
        {
            foreach (var bubble in _showingBubble.Values)
            {
                bubble.OffBubble();
            }
        }
    }
}
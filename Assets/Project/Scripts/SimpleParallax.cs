using System;
using UnityEngine;

namespace Project.Scripts
{
    public class SimpleParallax : MonoBehaviour
    {
        public Camera parallaxTarget;
        public float tileHeight = 3.6f;

        private int _childCount;
        private void Start()
        {
            _childCount = transform.childCount;
        }

        // Update is called once per frame
        void Update()
        {
            var parallaxPosition = parallaxTarget.transform.position;
            for (var childIndex = 0; childIndex < _childCount; childIndex++)
            {
                var currentTile = transform.GetChild(childIndex).gameObject;
                var currentTilePos = currentTile.transform.position;
                if ((parallaxPosition.y - currentTilePos.y) < tileHeight) continue;
                currentTile.transform.position = new Vector3(
                    0, 
                    currentTilePos.y + (_childCount * tileHeight), 
                    0);
            }
        }
    }
}

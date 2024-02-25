using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Effect.Level.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxLayer : MonoBehaviour
    {
        public float parallaxFactor;

        private float spriteWidth;

        private void Start()
        {
            var sprite = GetComponent<SpriteRenderer>().sprite;
            spriteWidth = sprite.texture.width / sprite.pixelsPerUnit;
            Debug.Log(spriteWidth + " for " + name);
        }

        public void Move(float delta)
        {
            Vector3 newPos = transform.localPosition;
            newPos.x -= delta * parallaxFactor;

            if (Mathf.Abs(transform.localPosition.x) - spriteWidth > 0)
            {
                transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
            }
            else
                transform.localPosition = newPos;
        }

    }
}

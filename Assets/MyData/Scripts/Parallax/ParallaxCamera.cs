using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Effect.Level.Parallax
{

    [ExecuteInEditMode]
    public class ParallaxCamera : MonoBehaviour
    {
        public delegate void ParallaxCameraDelegate(float deltaMovement);
        public ParallaxCameraDelegate onCameraTranslate;

        private float _oldPosition;

        void Start()
        {
            _oldPosition = transform.position.x;
        }

        void Update()
        {
            if (transform.position.x != _oldPosition)
            {
                if (onCameraTranslate != null)
                {
                    float delta = _oldPosition - transform.position.x;
                    onCameraTranslate(delta);
                }

                _oldPosition = transform.position.x;
            }
        }
    }
}

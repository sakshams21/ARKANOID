using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer Brick_SpriteRenderer;
        public int BrickType { get; private set; }

        public void SetBrickData(BrickData data)
        {
            BrickType= data.Type;
            Brick_SpriteRenderer.color = data.Color;
        }
    }
}
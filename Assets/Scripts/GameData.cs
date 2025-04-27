using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName ="Scriptable Object/Game Data",fileName ="GameData")]
    public class GameData:ScriptableObject
    {
        public int InitalLife;
        public BrickData[] Brick_DataList;

        public int MaxBrickTypes { get { return Brick_DataList.Length; } }

        //other Data
    }

    [Serializable]
    public class BrickData
    {
        public int Type=0;
        public Color Color=Color.white;
        public int Points=0;
    }
}

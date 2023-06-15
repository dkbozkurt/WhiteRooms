using System;
using UnityEngine;

namespace Game.Scripts.Helpers
{
    public enum ObjectColor
    {
        Red,
        Green,
        Blue
    }

    public enum ObjectShape
    {
        Pyramid,
        Cube,
        Sphere
    }

    public enum Answers
    {
        No,
        Yes
    }

    public enum Door
    {
        One,
        Two,
        Three,
        Four
    }

    [Serializable]
    public struct SectionInfo
    {
        public Answers Answer;
        public int ActualNumber;
        public ObjectColor TargetColor;
        public ObjectShape ShapeToFind;
        public int TotalElementCount;
        public float Duration;
        public Door[] DoorsToOpen;
    }

    public class Enums : MonoBehaviour
    {
    }
}
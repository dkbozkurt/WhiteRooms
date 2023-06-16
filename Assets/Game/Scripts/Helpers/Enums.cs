using System;
using Game.Scripts.Behaviours;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

namespace Game.Scripts.Helpers
{
    public enum ObjectColor
    {
        Red,
        Yellow,
        Blue
    }

    public enum ObjectShape
    {
        Pyramid,
        Cube,
        Sphere
    }

    public enum Answer
    {
        No,
        Yes
    }

    [Serializable]
    public struct ColorToSetData
    {
        public ObjectColor ObjectColorInfo;
        public Color ObjectTargetColor;
    }


    [Serializable]
    public struct QuestionInfo
    {
        public Answer Answer;
        public int QuestionNumber;
        public int ActualNumber;
        public ObjectColor TargetColor;
        public ObjectShape ShapeToFind;
        public int RandomObjectCount;
        public float Duration;
    }

    [Serializable]
    public struct ShapeAndBelonginsObjects
    {
        public ObjectShape RepresentiveShape;
        public ColoredObject[] ColoredObjects;
    }

    public class Enums : MonoBehaviour
    {
    }
}
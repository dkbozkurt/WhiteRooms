using System;
using Game.Scripts.Behaviours;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

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

    public enum Answer
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

    public enum Area
    {
        One,
        Two,
        Three,
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
        public int ActualNumber;
        public ObjectColor TargetColor;
        public ObjectShape ShapeToFind;
        public int RandomObjectCount;
        public float Duration;
        public Door[] DoorsToOpen;
    }

    [Serializable]
    public struct SectionProps
    {
        public Area AreaNumber;
        public ShapeAndBelonginsObjects[] ShapeAndBelonginsObjectsArray;
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
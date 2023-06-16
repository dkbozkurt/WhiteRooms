using System.Collections.Generic;
using Game.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataScriptableObject", order = 1)]
	public class GameDataScriptableObject : ScriptableObject
	{
		public string TutorialText =
			"Find out if the following statement is true. The room will give you the answer. You only have a limited amount of time to decide. Press \"Yes\" to start.";

		public Color DefaultObjectColor;
		public ColorToSetData[] ColorToSetDatas = new ColorToSetData[] { };
		
		public List<QuestionInfo> GameQuestions = new List<QuestionInfo>();
		
		public string GetSectionQuestion(int sectionIndex)
		{
			if (sectionIndex == -1)
			{
				return TutorialText;
			}
			
			string question = "";

			question = "There are " + GameQuestions[sectionIndex].ActualNumber.ToString() + " " +
			           GameQuestions[sectionIndex].TargetColor.ToString() + " " + GameQuestions[sectionIndex].ShapeToFind.ToString() + "s.";

			return question;
		}

		public Color GetColorByColorInfo(ObjectColor targetObjectColor)
		{
			for (int i = 0; i < ColorToSetDatas.Length; i++)
			{
				if (targetObjectColor == ColorToSetDatas[i].ObjectColorInfo)
				{
					return ColorToSetDatas[i].ObjectTargetColor;
				}				
			}

			return Color.black;
		}

	}
}

using System.Collections.Generic;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataScriptableObject", order = 1)]
	public class GameDataScriptableObject : ScriptableObject
	{
		public string TutorialText =
			"Find out if the following statement is true. The room will give you the answer. You only have a limited amount of time to decide. Press \"Yes\" to start.";

		public Material DefaultObjectMaterial;
		public MaterialData[] MaterialDatas = new MaterialData[] { };
		
		public List<SectionInfo> GameSections = new List<SectionInfo>();
		
		public string GetSectionQuestion(int sectionIndex)
		{
			if (sectionIndex == -1)
			{
				return TutorialText;
			}
			
			string question = "";

			question = "There are " + GameSections[sectionIndex].ActualNumber.ToString() + " " +
			           GameSections[sectionIndex].TargetColor.ToString() + " " + GameSections[sectionIndex].ShapeToFind.ToString() + "s.";

			return question;
		}

		public int GetRandomObjectCountToSpawn(int sectionIndex)
		{
			int randomObjectCount = 0;
			randomObjectCount = GameSections[sectionIndex].TotalElementCount - GameSections[sectionIndex].ActualNumber;
			return randomObjectCount;
		}
		
	}
}

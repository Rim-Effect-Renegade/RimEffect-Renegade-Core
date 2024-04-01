﻿using RimWorld;
using RimWorld.Planet;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Grammar;

namespace RimEffect
{
	public class QuestNode_NewPawnsArrive : QuestNode
	{
		[NoTranslate]
		public SlateRef<string> inSignal;

		public SlateRef<IEnumerable<Pawn>> pawns;

		public SlateRef<PawnsArrivalModeDef> arrivalMode;

		public SlateRef<bool> joinPlayer;

		public SlateRef<IntVec3?> walkInSpot;

		public SlateRef<string> customLetterLabel;

		public SlateRef<string> customLetterText;

		public SlateRef<RulePack> customLetterLabelRules;

		public SlateRef<RulePack> customLetterTextRules;

		public SlateRef<bool> isSingleReward;

		public SlateRef<bool> rewardDetailsHidden;

		private const string RootSymbol = "root";

		public override bool TestRunInt(Slate slate)
		{
			if (!slate.Exists("map"))
			{
				return false;
			}
			return true;
		}

		public override void RunInt()
		{
			Slate slate = QuestGen.slate;
			PawnsArrivalModeDef pawnsArrivalModeDef = arrivalMode.GetValue(slate) ?? PawnsArrivalModeDefOf.EdgeWalkIn;
			QuestPart_NewPawnsArrive pawnsArrive = new QuestPart_NewPawnsArrive();
			pawnsArrive.inSignal = (QuestGenUtility.HardcodedSignalWithQuestID(inSignal.GetValue(slate)) ?? QuestGen.slate.Get<string>("inSignal"));
			pawnsArrive.pawns.AddRange(pawns.GetValue(slate));
			pawnsArrive.arrivalMode = pawnsArrivalModeDef;
			pawnsArrive.joinPlayer = joinPlayer.GetValue(slate);
			pawnsArrive.mapParent = QuestGen.slate.Get<Map>("map").Parent;
			if (pawnsArrivalModeDef.walkIn)
			{
				pawnsArrive.spawnNear = (walkInSpot.GetValue(slate) ?? QuestGen.slate.Get<IntVec3?>("walkInSpot") ?? IntVec3.Invalid);
			}
			if (!customLetterLabel.GetValue(slate).NullOrEmpty() || customLetterLabelRules.GetValue(slate) != null)
			{
				QuestGen.AddTextRequest("root", delegate (string x)
				{
					pawnsArrive.customLetterLabel = x;
				}, QuestGenUtility.MergeRules(customLetterLabelRules.GetValue(slate), customLetterLabel.GetValue(slate), "root"));
			}
			if (!customLetterText.GetValue(slate).NullOrEmpty() || customLetterTextRules.GetValue(slate) != null)
			{
				QuestGen.AddTextRequest("root", delegate (string x)
				{
					pawnsArrive.customLetterText = x;
				}, QuestGenUtility.MergeRules(customLetterTextRules.GetValue(slate), customLetterText.GetValue(slate), "root"));
			}
			QuestGen.quest.AddPart(pawnsArrive);
			if (isSingleReward.GetValue(slate))
			{
				QuestPart_Choice questPart_Choice = new QuestPart_Choice();
				questPart_Choice.inSignalChoiceUsed = pawnsArrive.inSignal;
				QuestPart_Choice.Choice choice = new QuestPart_Choice.Choice();
				choice.questParts.Add(pawnsArrive);
				foreach (Pawn pawn in pawnsArrive.pawns)
				{
					choice.rewards.Add(new Reward_Pawn
					{
						pawn = pawn,
						detailsHidden = rewardDetailsHidden.GetValue(slate)
					});
				}
				questPart_Choice.choices.Add(choice);
				QuestGen.quest.AddPart(questPart_Choice);
			}
		}
	}
}
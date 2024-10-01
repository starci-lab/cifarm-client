using System;
using System.Collections.Generic;
using System.Linq;
using DuckSurvivor.Scripts.Configs;

namespace CiFarm.Scripts.Configs.DataClass
{
    public class TutorialsDetailConfig : SgConfigDataTable<TutorialDetailRecord>
    {
        protected override void RebuildIndex()
        {
            RebuildIndexByField<int>("Id");
        }

        public TutorialDetailRecord GetConfigSkillById(int id)
        {
            var record = Records.FirstOrDefault(x => x.Id == id);
            return record;
        }

        public List<TutorialDetailRecord> GetAllConfigSkill()
        {
            return Records;
        }
    }

    public class TutorialDetailRecord
    {
        public int                 Id;
        public TutorialsDetailType Type;
        public string              Localization;
        public string              Details;
        public string              CharacterId;
        public string              TargetClickId;
        public string              TargetImageId;

        // public bool TryGetTutorialDetailType(out TutorialsDetailType tutorialDetailType)
        // {
        //     return Enum.TryParse(Type, out tutorialDetailType);
        // }
    }

    public enum TutorialsDetailType
    {
        PopupMessage,
        ActionClick,
        PopupMessageImage
    }
}
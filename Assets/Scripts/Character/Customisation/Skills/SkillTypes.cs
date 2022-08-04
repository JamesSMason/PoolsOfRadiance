using PoR.Character.Customisation.Statistics;

namespace PoR.Character.Customisation.Skills
{
    public static class SkillTypes
    {
        private static string[] skillsArray = {
            "Acrobatics",
            "Animal Handling",
            "Arcana",
            "Athletics",
            "Deception",
            "History",
            "Insight",
            "Intimidation",
            "Investigation",
            "Medicine",
            "Nature",
            "Perception",
            "Performance",
            "Persuasion",
            "Religion",
            "Sleight Of Hand",
            "Stealth",
            "Survival"
        };

        private static Abilities[] skillsModifierStatArray = {
            Abilities.Dexterity,
            Abilities.Wisdom,
            Abilities.Intelligence,
            Abilities.Strength,
            Abilities.Charisma,
            Abilities.Intelligence,
            Abilities.Wisdom,
            Abilities.Charisma,
            Abilities.Intelligence,
            Abilities.Wisdom,
            Abilities.Intelligence,
            Abilities.Wisdom,
            Abilities.Charisma,
            Abilities.Charisma,
            Abilities.Intelligence,
            Abilities.Dexterity,
            Abilities.Dexterity,
            Abilities.Wisdom
        };

        public static string[] GetSkills()
        {
            return skillsArray;
        }

        public static string GetSkill(int index)
        {
            return skillsArray[index];
        }

        public static Abilities[] GetSkillModifierStats()
        {
            return skillsModifierStatArray;
        }

        public static Abilities GetSkillModifierStat(int index)
        {
            return skillsModifierStatArray[index];
        }
    }
}
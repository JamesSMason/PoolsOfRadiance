namespace PoR.Character.Customisation.Statistics
{
    public static class ProficiencyBonus
    {
        private static int[] proficiencyBonusArray = { 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

        public static int GetProficiencyBonus(int level)
        {
            return proficiencyBonusArray[level];
        }
    }
}
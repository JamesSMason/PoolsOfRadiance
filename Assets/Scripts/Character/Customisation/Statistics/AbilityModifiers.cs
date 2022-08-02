namespace PoR.Character.Customisation.Statistics
{
    public static class AbilityModifiers
    {
        private static int[] abilityScoreModifiers = { -5, -5, -4, -4, -3, -3, -2, -2, -1, -1, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

        public static int GetAbilityModifier(int abilityScore)
        {
            return abilityScoreModifiers[abilityScore];
        }
    }
}
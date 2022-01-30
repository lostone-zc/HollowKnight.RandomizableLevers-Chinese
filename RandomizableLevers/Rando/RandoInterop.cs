using System.Collections.Generic;
using RandomizerMod.Logging;

namespace RandomizableLevers.Rando
{
    internal static class RandoInterop
    {
        public static LeverRandomizationSettings Settings => RandomizableLevers.GS.RandoSettings;

        public static void HookRandomizer()
        {
            LogicPatcher.Hook();
            RandoMenuPage.Hook();
            RequestModifier.Hook();
            LateRandoChanges.Hook();

            RandomizerMod.Logging.SettingsLog.AfterLogSettings += AddLeverRandoSettings;

            // Add important levers to the condensed spoiler log
            CondensedSpoilerLogger.AddCategory("重要的拉干", (args) => true, ImportantLevers);
            CondensedSpoilerLogger.AddCategory("白宫拉干", (args) => true, PalaceLevers);
        }

        private static void AddLeverRandoSettings(RandomizerMod.Logging.LogArguments args, System.IO.TextWriter tw)
        {
            tw.WriteLine("Logging Lever Rando settings:");
            using Newtonsoft.Json.JsonTextWriter jtw = new(tw) { CloseOutput = false, };
            RandomizerMod.RandomizerData.JsonUtil._js.Serialize(jtw, Settings);
            tw.WriteLine();
        }

        private static readonly List<string> ImportantLevers = new()
        {
            LeverNames.Switch_Dirtmouth_Stag,
            LeverNames.Lever_Resting_Grounds_Stag,
            LeverNames.Lever_Mantis_Claw,
            LeverNames.Lever_Mantis_Lords_Access,
            LeverNames.Lever_Sanctum_Soul_Warrior,
            LeverNames.Lever_Sanctum_Bottom,
            LeverNames.Lever_Failed_Tramway_Left,
            LeverNames.Lever_Failed_Tramway_Right,
            LeverNames.Lever_Queens_Gardens_Ground_Block,
        };

        private static readonly List<string> PalaceLevers = new()
        {
            LeverNames.Lever_Palace_Entrance_Orb,
            LeverNames.Lever_Palace_Left_Orb,
            LeverNames.Lever_Palace_Right_Orb,
            LeverNames.Lever_Palace_Atrium,
            LeverNames.Lever_Palace_Right,
            LeverNames.Lever_Palace_Final,
            LeverNames.Lever_Path_of_Pain,
        };
    }
}

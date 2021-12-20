using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SCH
    {
        public const byte ClassID = 15;
        public const byte JobID = 28;

        public const uint
            FeyBless = 16543,
            Consolation = 16546,
            EnergyDrain = 167,
            Aetherflow = 166,
            Indomitability = 3583,
            Lustrate = 189,
            Excogitation = 7434,
            SacredSoil = 188,
            SummonEos = 17215,
            SummonSelene = 17216,
            WhisperingDawn = 16537,
            FeyIllumination = 16538,
            Dissipation = 3587,
            Aetherpact = 7437,
            SummonSeraph = 16545;

        public static class Buffs
        {
            public const ushort
                Recitation = 1896;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte Placeholder = 0;
        }
    }

    internal class ScholarFairyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarFairyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<SCHGauge>();
            if (!Service.BuddyList.PetBuddyPresent && gauge.SeraphTimer == 0)
                return IsEnabled(CustomComboPreset.ScholarSeleneOption) ? SCH.SummonSelene : SCH.SummonEos;

            return actionID;
        }
    }

    internal class ScholarSeraphConsolationFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarSeraphConsolationFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.FeyBless)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.SeraphTimer > 0)
                    return SCH.Consolation;
            }

            return actionID;
        }
    }

    internal class ScholarEnergyDrainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarEnergyDrainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.EnergyDrain)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.Aetherflow == 0)
                    return SCH.Aetherflow;
            }

            if (IsEnabled(CustomComboPreset.ScholarEverythingFeature))
            {
                if (HasEffect(SCH.Buffs.Recitation) && (actionID == SCH.Indomitability || actionID == SCH.Excogitation))
                    return actionID;
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.Aetherflow == 0)
                    return SCH.Aetherflow;
            }

            return actionID;
        }
    }
}

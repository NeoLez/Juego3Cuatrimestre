using Facts.Updaters;

namespace Facts {
    public class Facts {
        public static readonly Fact<int> TOTAL_JUMPS = new (0);
        private static readonly FactUpdater<int, Unit> TOTAL_JUMPS_UPDATER = new TriggerCounterInt(TOTAL_JUMPS, Events.ON_PLAYER_JUMPED);
    }
}
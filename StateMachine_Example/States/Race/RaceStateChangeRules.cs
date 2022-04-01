namespace States.Race
{
    public class RaceStateChangeRules
    {
        private readonly int _lapsCountToWin;

        public RaceStateChangeRules(int lapsCountToWin)
        {
            _lapsCountToWin = lapsCountToWin;
        }

        public bool IsChangeToFinishState(RacerLapInfo playerLapInfo)
        {
            if (playerLapInfo.Lap >= _lapsCountToWin)
                return true;
            else
                return false;
        }
    }
}

namespace Sea_Battle.Classes
{
    class Cell
    {
        public bool isBlownUp { get; private set; } = false;
        public bool hasNoShip { get; private set; } = true;

        public void prepareForGame()
        {
            isBlownUp = false;
        }

        public void blowUp()
        {
            isBlownUp = true;
        }

        public void takePlace()
        {
            hasNoShip = false;
        }

    }
}

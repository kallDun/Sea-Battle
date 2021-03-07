
using Newtonsoft.Json;

namespace Sea_Battle.Classes
{
    class Cell
    {
        [JsonProperty]
        public bool isBlownUp { get; private set; } = false;
        [JsonProperty]
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

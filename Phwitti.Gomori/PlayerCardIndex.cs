
namespace Phwitti.Gomori
{
    public enum PlayerCardIndex
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
    }

    public static class PlayerCardIndexUtils
    {
        private static PlayerCardIndex[] s_arAll = new PlayerCardIndex[] {
            PlayerCardIndex.First, PlayerCardIndex.Second,
            PlayerCardIndex.Third, PlayerCardIndex.Fourth,
            PlayerCardIndex.Fifth };

        public static PlayerCardIndex[] All
            => s_arAll;
    }
}

public static class Rules
{
    private const int NUMBER_MIN_KEEP_ALIVE = 2;
    private const int NUMBER_MAX_KEEP_ALIVE = 3;
    private const int NUMBER_RETURN_ALIVE = 3;

    public static bool Born(int numberNeighbours)
    {
        return numberNeighbours == NUMBER_RETURN_ALIVE;
    }

    public static bool Kill(int numberNeighbours)
    {
        return numberNeighbours < NUMBER_MIN_KEEP_ALIVE || numberNeighbours > NUMBER_MAX_KEEP_ALIVE;
    }
       
}

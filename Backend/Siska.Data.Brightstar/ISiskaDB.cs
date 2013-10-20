namespace Siska.Data
{
    using Siska.Data.BDao;

    public interface ISiskaDB
    {
        bool IsInitialised { get; }
        BsContext BsContext { get; }
    }
}

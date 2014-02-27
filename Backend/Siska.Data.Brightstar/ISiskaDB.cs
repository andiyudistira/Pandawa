namespace Siska.Data
{
    using Siska.Data.Dao;

    public interface ISiskaDB
    {
        bool IsInitialised { get; }
        BsContext BsContext { get; set; }
    }
}

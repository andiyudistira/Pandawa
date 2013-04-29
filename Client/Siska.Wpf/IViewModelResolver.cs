namespace Siska.Wpf
{
    public interface IViewModelResolver
    {
        object Resolve(string viewModelName);
    }
}

namespace Module.App.Scripts.Factory
{
    public interface IFactoryUnitInitializable<TData>
    {
        void InitializeUnit(TData data);
    }
}
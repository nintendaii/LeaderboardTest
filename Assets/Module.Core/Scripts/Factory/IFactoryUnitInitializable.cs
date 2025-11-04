namespace Module.Core.Scripts.Factory
{
    public interface IFactoryUnitInitializable<TData>
    {
        void UnitInitialize(TData data);
    }
}
namespace Module.Core {
    public interface IBindComponentInHierarchy { }
    public interface IBindComponentFlagAsSingle { }
    public interface IBindComponentFlagNonLazy { }
    public interface IBindComponent : IBindComponentInHierarchy, IBindComponentFlagAsSingle, IBindComponentFlagNonLazy { }
}
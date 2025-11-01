namespace Module.Core.CommandSignal
{
    public interface ICommand {
        void Execute();
    }
    
    public interface ICommandWithParameter {
        void Execute(ISignal signal);
    }
}
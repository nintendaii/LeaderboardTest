namespace Module.Core.Scripts.CommandSignal
{
    public interface ICommand {
        void Execute();
    }
    
    public interface ICommandWithParameter {
        void Execute(ISignal signal);
    }
}
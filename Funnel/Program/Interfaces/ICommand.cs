
namespace Funnel
{
    public interface ICommand
    {

        /// <summary>
        /// The command read to execute
        /// </summary>
        string Command { get; }

        /// <summary>
        /// Describes the command functionality
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes the command
        /// </summary>
        void Execute();

    }
}

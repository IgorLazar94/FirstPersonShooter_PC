namespace PauseSystem
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void PauseGame();
        void ResumeGame();
    }
}

using UnityEngine;

namespace PauseSystem
{
    public class PauseService : IPauseService
    {
        private bool isPaused = false;

        public bool IsPaused => isPaused;
        
        public void PauseGame()
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

        public void ResumeGame()
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }
}

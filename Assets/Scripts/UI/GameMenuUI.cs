using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Overcooked.UI
{
    public class GameMenuUI : BaseGameMenuUI
    {
        public void Restart()
        {
            string levelName = "";
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name != "GameScene")
                    levelName = SceneManager.GetSceneAt(i).name;
            }
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, levelName } });
        }

        public override void ExitFromLevel()
        {
            EventManager.TriggerEvent(EventType.LoadScene, new Dictionary<EventMessageType, object> { { EventMessageType.SceneName, "GlobalMap" } });
        }
    }
}

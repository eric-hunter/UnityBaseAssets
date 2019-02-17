using UnityEngine;

/*
 * Description: Copied (thank you) from Unity wiki, looked like there was more unity related
 * management of objects than a simple singleton implementation would require.
 * 
 * http://wiki.unity3d.com/index.php/Singleton
 */

namespace Unity2DBase
{

}
public class SingletonUnityScript<T> : MonoBehaviour where T : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    // Check to see if we're about to be destroyed.
    private static bool shuttingDown;
    private static object lockObject = new object();
    private static T instance;

    #endregion

    #region PUBLIC PROPERTIES

    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            //single thread access to the singleton instance
            lock (lockObject)
            {
                if (instance == null)
                {
                    // Search for existing instance.
                    instance = (T)FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T) + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    #endregion

    #region UNITY MESSAGES 

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }


    private void OnDestroy()
    {
        shuttingDown = true;
    }

    #endregion
}

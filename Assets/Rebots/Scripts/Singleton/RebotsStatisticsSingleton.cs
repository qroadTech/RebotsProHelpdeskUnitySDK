using HelpDesk.Sdk.Library.Configuration;
using HelpDesk.Sdk.Unity.Library.Objects;
using UnityEngine;

namespace Rebots.HelpDesk
{
    /// <summary>
    /// Statistics Method singleton.
    /// 
    /// This singletone class globally used in sample game. Instance will
    /// constructed after <see cref="RebotsSettingManager.HelpdeskStatisticsAuthorize"/>.
    /// </summary>
    public class RebotsStatisticsSingleton : MonoBehaviour
    {
        private static UnityRebotsProMaxAddStatistics request;
        private static RebotsStatisticsSingleton instance;
        private static object _lock = new Object();

        public static RebotsStatisticsSingleton Instance
        {
            get 
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        instance = go.AddComponent<RebotsStatisticsSingleton>();
                    }
                }

                return instance;
            }
        }

        public void Initialize(Configurations configurations) 
        {
            request = new UnityRebotsProMaxAddStatistics(configurations);
        }

        /// <summary>
        /// <example>
        /// parameter key use only: 
        /// <code>
        /// RebotsStatisticsSingleton.Instance.Send("PlayerLose");
        /// </code>
        /// parameter value use: 
        /// <code>
        /// RebotsStatisticsSingleton.Instance.Send("UsePotion", item.name);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="key">key strings what you set from RebotsPro Web solution(workspace).</param>
        /// <param name="value">detail value of key</param>
        public void Send(string key, string value = null) 
        {
            if (request == null) 
            {
                return;
            }

            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (key.Length < 4 || key.Length > 32)
            {
                // key must be 4 between 32 length.
                return;
            }

            if (!string.IsNullOrEmpty(value) && value.Length > 64) 
            {
                // Value can be null, empty or lesser than 64 length.
                return;
            }

            Debug.Log($"Sending statistic data. Data: Key : {key}, Value: {value}");
            StartCoroutine(request.Add(key, value));
            Debug.Log("Finished to sending statistic data.");
        }
    }
}

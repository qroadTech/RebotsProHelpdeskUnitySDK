using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rebots.HelpDesk
{
    [Serializable]
    public class RebotsParameterData
    {
        string userAuthKey;
        /// <summary>
        /// User Unique IDentifier (Required)
        /// </summary>
        public string UserAuthKey
        {
            get
            {
                return userAuthKey;
            }
            set
            {
                userAuthKey = value;
                if (parameters.ContainsKey("user_id"))
                {
                    parameters["user_id"] = userAuthKey;
                }
            }
        }

        string userName;
        /// <summary>
        /// User Name (Options)
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                if (parameters.ContainsKey("user_name"))
                {
                    parameters["user_name"] = userName;
                }
            }
        }

        string userEmail;
        /// <summary>
        /// User Email (Options)
        /// </summary>
        public string UserEmail
        {
            get
            {
                return userEmail;
            }
            set
            {
                userEmail = value;
                if (parameters.ContainsKey("email"))
                {
                    parameters["email"] = userEmail;
                }
            }
        }

        /// <summary>
        /// Inquiry Parameter Key Value Dictionary
        /// </summary>
        public Dictionary<string, string> parameters { get; private set; }

        string m_parameterJsonStr;
        public string parameterJsonStr
        {
            get 
            {
                m_parameterJsonStr = ToJson();
                return m_parameterJsonStr; 
            }
        }

        public RebotsParameterData()
        {
            parameters = new()
            {
                { "user_id", "" },
                { "user_name", "" },
                { "email", "" }
            };
        }

        public string ToJson()
        { 
            var entries = parameters.Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToArray();
            return "{" + string.Join(",", entries) + "}";
        }


    }
}

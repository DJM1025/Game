using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripting
{
    [Serializable]
    class MasterRunningScriptList
    {
        private static List<BaseScript> _runningScripts = new List<BaseScript>();

        public static void AddScript(BaseScript script)
        {
            lock (_runningScripts)
            {
                _runningScripts.Add(script);
            }
        }

        public static void Remove(BaseScript script)
        {
            lock (_runningScripts)
            {
                _runningScripts.Remove(script);
            }
        }

        public static void StopAllScripts()
        {
            BaseScript.AllScriptsEnding = true;
            _closing = true;
            lock (_runningScripts)
            {
                foreach (var script in _runningScripts)
                {
                    script.StopScript();
                }
            }
        }

        public static Boolean Closing
        {
            get
            {
                return _closing;
            }
        }

        private static Boolean _closing = false;
    }
}

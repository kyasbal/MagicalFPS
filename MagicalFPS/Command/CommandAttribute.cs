using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagicalFPS.Command
{
    [AttributeUsage(AttributeTargets.Method)]
    class CommandAttribute:Attribute
    {
        private string commandName;

        private int argCount;

        private string description;

        public string Description
        {
            get { return description; }
        }

        public CommandAttribute(string commandName, int argCount,string description="")
        {
            this.commandName = commandName;
            this.argCount = argCount;
            this.description = description;
        }

        public string CommandName
        {
            get { return commandName; }
        }

        public int ArgCount
        {
            get { return argCount; }
        }
    }
}

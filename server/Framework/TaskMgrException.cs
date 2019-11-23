using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class TaskMgrException : Exception
    {
        public string Code { get; }

        public TaskMgrException()
        {
        }

        public TaskMgrException(string code)
        {
            Code = code;
        }

        public TaskMgrException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public TaskMgrException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public TaskMgrException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public TaskMgrException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}

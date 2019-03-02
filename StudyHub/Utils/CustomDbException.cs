using System;
using System.Data.Common;

namespace StudyHub.Utils
{
    public class CustomDbException : DbException
    {
        public CustomDbException(string error) : base(error) { }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid Id.") { }
        public InvalidIdException(string message) : base(message) { }
        public InvalidIdException(string message, Exception innerException) : base(message, innerException) { }
    }
}

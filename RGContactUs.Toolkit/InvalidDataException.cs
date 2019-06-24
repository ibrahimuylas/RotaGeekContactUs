using System;
using System.Collections.Generic;
using System.Text;

namespace RGContactUs.Toolkit
{
    public class InvalidDataException : ApplicationException
    {

        public InvalidDataException()
        {
        }
        public InvalidDataException(string err) : base(err)
        {

        }
    }
}

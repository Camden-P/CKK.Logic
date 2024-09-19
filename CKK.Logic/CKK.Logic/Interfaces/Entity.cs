using CKK.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    public abstract class Entity
    {
        // Instance Variables
        private int _id;
        private string _name;
        
        // Properties
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value >= 0)
                {
                    _id = value;
                }
                else
                {
                    throw new InvalidIdException();
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}

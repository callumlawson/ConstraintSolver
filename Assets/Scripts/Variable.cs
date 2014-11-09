using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Variable
    {
        private int LastSaveFramePointer = -1;
        public UInt64 Values { get; set; }
        public HashSet<Constraint> Constraints { get; set; }

        public bool Narrow(UInt64 suggestedValues) //This is hopefully a subset of Values
        {
            var commonValues = suggestedValues & Values;

            if (commonValues == 0)
            {
                return false;
            }

            if (Values != commonValues)
            {
                foreach (var constraint in Constraints)
                {
                    if (!constraint.Propagate(this))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsUnique()
        {
            return (Values != 0) && ((Values & (Values - 1)) == 0);
        }


    }
}
using System.Collections.Generic;

namespace Assets.Scripts.Constraints
{
    internal class EqualityConstraint : Constraint
    {
        public EqualityConstraint(List<Variable> variables) : base(variables)
        {
        }

        public override bool Propagate(Variable unneeded)
        {
            foreach (var variable in Variables)
            {
                foreach (var otherVariable in Variables)
                {
                    if (variable.Narrow(otherVariable.Values))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
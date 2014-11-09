using System.Collections.Generic;

namespace Assets.Scripts
{
    public abstract class Constraint
    {
        public readonly List<Variable> Variables;

        protected Constraint(List<Variable> variables)
        {
            Variables = variables;
        }

        public abstract bool Propagate(Variable updatedVariable);
    }
}
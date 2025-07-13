using Conditions;

namespace Achievements {
    public class Achievement {
        private ICondition _condition;
        public string Name { get; private set; }

        public Achievement(string name, ICondition condition) {
            _condition = condition;
            Name = name;
        }

        public bool Evaluate() {
            bool result = _condition.Evaluate();
            return result;
        }
    }
}
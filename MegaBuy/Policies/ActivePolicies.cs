using System.Collections.Generic;
using System.Linq;

namespace MegaBuy.Policies
{
    public sealed class ActivePolicies
    {
        private readonly List<Policy> _policies = new List<Policy>();

        public int Count => _policies.Count;

        public void Add(Policy policy)
        {
            _policies.Add(policy);
        }

        public void Remove(Policy policy)
        {
            _policies.RemoveAll(x => x.Text.Equals(policy.Text));
        }

        public List<string> GetPolicyTexts()
        {
            return _policies.Select(x => x.Text).ToList();
        }

        public List<string> GetPolicyTexts(int startingIndex, int count)
        {
            return _policies
                .Select(x => x.Text)
                .Skip(startingIndex - 1)
                .Take(count)
                .ToList();
        }
    }
}

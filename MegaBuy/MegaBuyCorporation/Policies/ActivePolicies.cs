using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.MegaBuyCorporation.Policies
{
    public sealed class ActivePolicies
    {
        private readonly List<Policy> _policies = new List<Policy>();

        public int Count => _policies.Count;

        public ActivePolicies(IEnumerable<Policy> policies)
        {
            _policies.AddRange(policies);
        }

        public void Add(IEnumerable<Policy> policies)
        {
            _policies.AddRange(policies);
            World.Publish(new PoliciesChanged());
        }

        public void Add(Policy policy)
        {
            _policies.Add(policy);
            World.Publish(new PoliciesChanged());
        }

        public void Remove(Policy policy)
        {
            _policies.RemoveAll(x => x.Text.Equals(policy.Text));
            World.Publish(new PoliciesChanged());
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

        public List<Policy> GetViolations(CallResolution resolution, Call call)
        {
            return _policies.Where(x => !x.MeetsPolicy(resolution, call)).ToList();
        }

        public void Clear()
        {
            _policies.Clear();
        }
    }
}

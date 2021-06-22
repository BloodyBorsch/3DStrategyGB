using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Abstractions;
using UniRx;


namespace Core
{
    public class AutoAttackGlobalBehavior : MonoBehaviour
    {
        private struct AutoAttackCommandInfo
        {
            public BaseUnit Attacker;
            public IAttackable Target;

            public AutoAttackCommandInfo(BaseUnit attacker, IAttackable target)
            {
                Attacker = attacker;
                Target = target;
            }
        }

        public static ConcurrentDictionary<BaseUnit, IFractionMember> Units = new ConcurrentDictionary<BaseUnit, IFractionMember>();

        private Subject<AutoAttackCommandInfo> _attackTarget = new Subject<AutoAttackCommandInfo>();

        protected void Start()
        {
            _attackTarget.ObserveOnMainThread().Subscribe(PerformAutoAttack).AddTo(this);
        }

        protected void Update()
        {
            Parallel.ForEach(Units, AttackNearTarget);
        }

        private void PerformAutoAttack(AutoAttackCommandInfo info)
        {
            if (info.Attacker != null) info.Attacker.AttackTarget(info.Target);
        }

        private void AttackNearTarget(KeyValuePair<BaseUnit, IFractionMember> unitWithinfo)
        {
            var unit = unitWithinfo.Key;
            var fraction = unitWithinfo.Value;

            if (!unit.CanPerformAutoAttack()) return;

            foreach (var kvp in Units)
            {
                if (kvp.Value.Team == fraction.Team) continue;

                var otherTarget = kvp.Key;
                var distance = (unit.Position - otherTarget.Position).sqrMagnitude;

                if (distance < unit.VisionRange)
                {
                    _attackTarget.OnNext(new AutoAttackCommandInfo(unit, otherTarget));
                    break;
                }
            }
        }
    }
}
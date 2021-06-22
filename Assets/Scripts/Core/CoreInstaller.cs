using UnityEngine;
using Zenject;


namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProduceUnitExecutorChomper>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<ProduceUnitExecutorSpitter>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<AttackCommandExecutor>().FromComponentsInHierarchy().AsTransient();
        }
    }
}
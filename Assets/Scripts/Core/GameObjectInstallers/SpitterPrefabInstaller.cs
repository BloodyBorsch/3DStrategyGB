using Zenject;
using UnityEngine;


namespace Core
{
    public class SpitterPrefabInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<float>().WithId("Distance").FromInstance(5.0f);
            Container.Bind<float>().WithId("AttackSpeed").FromInstance(1.0f);
            Container.Bind<float>().WithId("AttackDamage").FromInstance(7.0f);
        }
    }
}
using InputSystem;
using UnityEngine;
using Utils;
using Zenject;
using Abstractions;


namespace Model
{
    public class ModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsContext _context;
        [SerializeField] private Vector3Value _currentGroundPosition;
        [SerializeField] private SelectedItem _currentSelectedItem;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_context).AsSingle();
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_currentGroundPosition).AsSingle();
            Container.Bind<IAwaitable<ISelectableItem>>().FromInstance(_currentSelectedItem).AsSingle();
            Container.Bind<ButtonPanel>().AsTransient();
            Container.Bind<Vector3Value>().FromInstance(_currentGroundPosition).AsSingle();
            Container.Bind<SelectedItem>().FromInstance(_currentSelectedItem).AsSingle();
        }
    }
}

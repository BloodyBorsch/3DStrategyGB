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
        [SerializeField] private AttackableValue _currentTarget;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_context).AsSingle();
            Container.Bind<CommandCreatorBase<IProduceUnitCommandChomper>>().To<ProduceChomperCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IProduceUnitCommandSpitter>>().To<ProduceSpitterCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_currentGroundPosition).AsSingle();
            Container.Bind<IAwaitable<ISelectableItem>>().FromInstance(_currentSelectedItem).AsSingle();
            Container.Bind<IAwaitable<IAttackable>>().FromInstance(_currentTarget).AsSingle();            
            Container.Bind<ButtonPanel>().AsSingle();
            Container.Bind<UnitProductionPanel>().AsSingle();
            Container.Bind<Vector3Value>().FromInstance(_currentGroundPosition).AsSingle();
            Container.Bind<SelectedItem>().FromInstance(_currentSelectedItem).AsSingle();
            Container.Bind<AttackableValue>().FromInstance(_currentTarget).AsSingle();

            SetUnitsInfo();
        }

        private void SetUnitsInfo()
        {
            Container.Bind<int>().WithId("Chomper").FromInstance(100);
            Container.Bind<Sprite>().WithId("Chomper").FromInstance(_context.GetSprite("Chomper"));
        }
    }
}

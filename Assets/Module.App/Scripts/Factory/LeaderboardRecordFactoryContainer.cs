using Module.App.Scripts.Controllers.Leaderboard.Record;
using Module.App.Scripts.Data;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Module.App.Scripts.Factory
{
    public class LeaderboardRecordFactoryContainer: ControllerMonoBase
    {
        [Inject] private readonly UnitLeaderboardRecordFactory unitLeaderboardRecordControllerFactory;
        
        private IObjectPool<UnitLeaderboardRecordController> objectPool;

        public override void Initialize()
        {
            base.Initialize();
            objectPool = new ObjectPool<UnitLeaderboardRecordController>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject);
        }

        public override void Dispose()
        {
            objectPool.Clear();
            base.Dispose();
        }

        public UnitLeaderboardRecordController CreateRecord(LeaderboardRecordData recordData, Transform parent)
        {
            var unit = objectPool.Get();
            unit.Transform.SetParent(parent); //no
            unit.Transform.localScale = Vector3.one;
            return unit;
        }

        public void ReleaseRecord(UnitLeaderboardRecordController recordController)
        {
            objectPool.Release(recordController);
        }
        private UnitLeaderboardRecordController CreatePooledItem()
        {
            var unit = unitLeaderboardRecordControllerFactory.Create();
            unit.Transform.SetParent(Transform);
            return unit;
        }
        private void OnTakeFromPool(UnitLeaderboardRecordController unit)
        {
            unit.ShowComponent();
        }

        private void OnReturnedToPool(UnitLeaderboardRecordController unit)
        {
            unit.HideComponent();
        }

        private void OnDestroyPoolObject(UnitLeaderboardRecordController unit)
        {
            Destroy(unit);
        }
    }
}
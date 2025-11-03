using System.Collections.Generic;
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
        [Inject] private readonly UnitLeaderboardRecordFactory _unitLeaderboardRecordControllerFactory;
        
        private IObjectPool<UnitLeaderboardRecordController> _objectPool;
        private List<UnitLeaderboardRecordController> _unitsList = new();

        public override void Initialize()
        {
            base.Initialize();
            _objectPool = new ObjectPool<UnitLeaderboardRecordController>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject);
        }

        public override void Dispose()
        {
            _objectPool.Clear();
            base.Dispose();
        }

        public UnitLeaderboardRecordController CreateRecord(LeaderboardRecordData recordData, Transform parent)
        {
            var unit = _objectPool.Get();
            _unitsList.Add(unit);
            unit.Transform.SetParent(parent);
            unit.Transform.localScale = Vector3.one;
            return unit;
        }

        public void ClearContainer()
        {
            foreach (var u in _unitsList)
            {
                u.Transform.SetParent(Transform);
                ReleaseRecord(u);
            }
            _unitsList.Clear();
        }

        public void ReleaseRecord(UnitLeaderboardRecordController recordController)
        {
            _objectPool.Release(recordController);
            recordController.Reset();
        }
        private UnitLeaderboardRecordController CreatePooledItem()
        {
            var unit = _unitLeaderboardRecordControllerFactory.Create();
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
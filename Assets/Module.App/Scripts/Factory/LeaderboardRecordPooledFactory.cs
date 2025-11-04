using Module.App.Scripts.Controllers.Leaderboard.Record;
using Module.App.Scripts.Data;
using Module.Core.Scripts.Factory;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.Factory
{
    public class LeaderboardRecordPooledFactory 
        : PooledFactoryBase<LeaderboardRecordController>
    {
        [Inject] private readonly UnitLeaderboardRecordFactory _unitLeaderboardRecordControllerFactory;

        protected override LeaderboardRecordController CreatePooledItem()
        {
            var unit = _unitLeaderboardRecordControllerFactory.Create();
            unit.Transform.SetParent(Transform);
            return unit;
        }

        public LeaderboardRecordController CreateRecord(LeaderboardRecordData data, Transform parent)
        {
            var record = CreateItem(parent);
            record.UnitInitialize(data);
            return record;
        }
    }

}
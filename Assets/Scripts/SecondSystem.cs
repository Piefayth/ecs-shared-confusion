using Unity.Entities;
using Unity.Jobs;
using System.Collections.Generic;

[UpdateAfter(typeof(FirstSystem))]
public class SecondSystem : JobComponentSystem {
    EntityManager entityManager;
    List<SomeSharedType> allSharedTypes;

    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        return inputDeps;
    }

    public NativeArrayToucher buildJobs() {
        allSharedTypes.Clear();
        entityManager.GetAllUniqueSharedComponentData(allSharedTypes);

        return new NativeArrayToucher { dataHolder = allSharedTypes[1] };
    }

    protected override void OnCreateManager() {
        entityManager = World.GetOrCreateManager<EntityManager>();
        allSharedTypes = new List<SomeSharedType>();
    }

    protected override void OnDestroyManager() {
    }
}

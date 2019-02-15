using Unity.Entities;
using Unity.Jobs;
using System.Collections.Generic;

[UpdateBefore(typeof(SecondSystem))]
public class FirstSystem : JobComponentSystem {
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

public struct NativeArrayToucher : IJob {
    public SomeSharedType dataHolder;

    public void Execute() {
        dataHolder.data[0] = 5;
    }
}
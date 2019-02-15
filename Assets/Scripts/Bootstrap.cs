using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;

public class Bootstrap : JobComponentSystem {
    NativeArray<int> data;
    EntityManager entityManager;

    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        NativeArrayToucher firstSystemJobs = World.GetOrCreateManager<FirstSystem>().buildJobs();
        NativeArrayToucher secondSystemJobs = World.GetOrCreateManager<SecondSystem>().buildJobs();

        JobHandle firstSystemWork = firstSystemJobs.Schedule(inputDeps);
        JobHandle secondSystemWork = secondSystemJobs.Schedule(firstSystemWork);
        return secondSystemWork;
    }

    protected override void OnCreateManager() {
        entityManager = World.GetOrCreateManager<EntityManager>();
        data = new NativeArray<int>(new int[] { 3 }, Allocator.Persistent);

        Entity e = entityManager.CreateEntity(typeof(SomeSharedType));
        entityManager.SetSharedComponentData(e, new SomeSharedType {
            data = data
        });
    }

    protected override void OnDestroyManager() {
        data.Dispose();
    }
}

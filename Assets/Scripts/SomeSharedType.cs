using Unity.Entities;
using Unity.Collections;

public struct SomeSharedType : ISharedComponentData {
    public NativeArray<int> data;
}
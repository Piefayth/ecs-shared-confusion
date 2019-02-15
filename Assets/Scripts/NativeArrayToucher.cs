using Unity.Jobs;

public struct NativeArrayToucher : IJob {
    public SomeSharedType dataHolder;

    public void Execute() {
        dataHolder.data[0] = 5;
    }
}
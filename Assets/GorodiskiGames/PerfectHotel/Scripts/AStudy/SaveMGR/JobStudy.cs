using Unity.Jobs;
using Unity.Collections;
public class JobStudy
{
    public int id;
    public string name;
    public string description;
    public NativeArray<int> inputData;
    public NativeArray<int> outputData;
    void test()
    {
        int count = 1000;
        inputData = new NativeArray<int>(count, Allocator.TempJob);
        outputData = new NativeArray<int>(count, Allocator.TempJob);
        for (int i = 0; i < count; i++)
        {
            inputData[i] = i;
        }
        JobDemo job = new JobDemo
        {
            inputData = inputData,
            outputData = outputData
        };

        var handle = job.Schedule(count, 64);
        handle.Complete(); //等待作业完成
        inputData.Dispose();
        outputData.Dispose();
    }

}
public struct JobDemo : IJobParallelFor
{
    public NativeArray<int> inputData;
    public NativeArray<int> outputData;
    public void Execute(int index)
    {
        outputData[index] = inputData[index] * 2;
    }
}
using System;
using Unity.Profiling;
using UnityEngine;

public class GCPerformanceRecorder : MonoBehaviour
{
    public static GCPerformanceRecorder Instance { get; private set; }

    private ProfilerRecorder gcAllocRecorder;

    private int gcCount;
    private int lastGCCount;

    private int spawnCount;
    private int destroyCount;

    private float elapsedTime;

    private float maxFrameTime;

    private float totalFrameTime;
    private int frameCount;

    public long TotalGCAlloc { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        gcAllocRecorder = ProfilerRecorder.StartNew(
            ProfilerCategory.Memory,
            "GC Allocated In Frame");

        lastGCCount = GC.CollectionCount(0);
    }

    void Update()
    {
        elapsedTime += Time.unscaledDeltaTime;

        //--------------------------------
        // GC.Alloc
        //--------------------------------

        if (gcAllocRecorder.Valid)
        {
            TotalGCAlloc += gcAllocRecorder.LastValue;
        }

        //--------------------------------
        // GC回数
        //--------------------------------

        int current = GC.CollectionCount(0);

        if (current != lastGCCount)
        {
            gcCount += current - lastGCCount;
            lastGCCount = current;
        }

        //--------------------------------
        // FrameTime
        //--------------------------------

        float frame = Time.unscaledDeltaTime;

        totalFrameTime += frame;
        frameCount++;

        if (frame > maxFrameTime)
            maxFrameTime = frame;
    }

    public void AddSpawn()
    {
        spawnCount++;
    }

    public void AddDestroy()
    {
        destroyCount++;
    }

    public void PrintResult()
    {
        float avgFrame = totalFrameTime / frameCount;
        float avgFPS = 1f / avgFrame;

        Debug.Log(
$@"============================
GC検証結果
============================

生成数        : {spawnCount:N0}
削除数        : {destroyCount:N0}

GC.Alloc      : {TotalGCAlloc / 1024f / 1024f:F2} MB
GC回数        : {gcCount}

最大FrameTime : {maxFrameTime * 1000f:F2} ms
平均FPS       : {avgFPS:F1}

実行時間      : {elapsedTime:F2} sec
============================");
    }

    void OnDisable()
    {
        gcAllocRecorder.Dispose();
    }
}
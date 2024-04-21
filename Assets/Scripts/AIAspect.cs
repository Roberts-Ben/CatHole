using Unity.Entities;
using Unity.Transforms;

public readonly partial struct AIAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRW<LocalTransform> _transform;
    private readonly RefRO<AgentProperties> _AIProperties;
    private readonly RefRW<AIRandom> _AIRandom;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool : MonoBehaviour 
{
	private AgentItemPool _agentItemPool;

	public ObjectPool(AgentItemPool agentItemPool)
	{
		_agentItemPool = agentItemPool;
	}

	public AgentItemUI GetAgentItem()
	{
		return _agentItemPool.Spawn();
	}

	public void ReturnAgentItem(AgentItemUI item)
	{
		_agentItemPool.Despawn(item);
	}

	public class AgentItemPool: MonoMemoryPool<AgentItemUI> {}
}

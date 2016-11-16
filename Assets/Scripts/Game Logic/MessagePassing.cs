using UnityEngine;
using System.Collections;

public interface MessagePassing {
	void collisionWith (Collider c); 
}

public static class MessagePassingHelper {
	public static void passMessageOnCollision(MonoBehaviour caster, Collider col)
	{
		MessagePassing entity = null;
		MonoBehaviour[] entities = col.gameObject.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour mono in entities) {
			entity = mono as MessagePassing;
			if (entity != null)
				break;
		}

		if ( entity != null)
			entity.collisionWith (caster.GetComponent<Collider>());
	}
}
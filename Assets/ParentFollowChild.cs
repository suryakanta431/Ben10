using UnityEngine;

public class ParentFollowChild : MonoBehaviour
{
    void LateUpdate()
    {
        Transform active = GetActiveChild();
        if (active == null) return;

        foreach (Transform child in transform)
        {
            if (child == active) continue;

            child.position = active.position;
            child.rotation = active.rotation;
        }
    }

    Transform GetActiveChild()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                return child;
        }
        return null;
    }
}

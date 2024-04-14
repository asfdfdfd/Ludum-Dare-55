using UnityEngine;

public class HealthPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabHeartIconPanelItem;

    internal void SetHealth(int healthNew)
    {
        var healthOld = gameObject.transform.childCount;

        if (healthNew < healthOld)
        {
            var removeItemsCount = healthOld - healthNew;

            for (int i = 0; i < removeItemsCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = healthOld; i < healthNew; i++)
            {
                GameObject heartIconPanelItem = Instantiate(_prefabHeartIconPanelItem);

                heartIconPanelItem.transform.SetParent(gameObject.transform);
            }
        }
    }
}

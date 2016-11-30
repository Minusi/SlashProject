using UnityEngine;
using System.Collections;
using System.Threading;

public enum IType { ARMOR, KNIFE, SPEED };
public class ItemManager : MonoBehaviour {

    public IType itype;
    public GameObject[] items;
    Item_struct[] items_struct;

    public struct Item_struct
    {
        public GameObject item;
        public IType itype;
    }

    void ItemDistributor()
    {
        int i = 0;
        while (items[i] != null)
        {
            i++;
        }
        items_struct = new Item_struct[i];
        i = 0;
        while (items[i] != null)
        {
            int ran = Random.Range(0, 3);
            switch (ran)
            {
                case 0:
                    items_struct[i].itype = IType.ARMOR;
                    break;
                case 1:
                    items_struct[i].itype = IType.KNIFE;
                    break;
                case 2:
                    items_struct[i].itype = IType.SPEED;
                    break;
            }
            items_struct[i].item = items[i];
            i++;
        }
    }

    public void ItemActive(GameObject item)
    {
        int i = 0;
        while (items[i] != null) {
            if (items_struct[i].item == item)
            {
                switch (items_struct[i].itype)
                {
                    case IType.ARMOR:
                        Player.isArmed = true;
                        break;
                    case IType.KNIFE:
                        Player.bulletCount = 5;
                        break;
                    case IType.SPEED:
                        Player.speed=6;
                        SpeedItem();
                        break;
                }
            }
            i++;
        }
    }

    void SpeedItem()
    {
        Thread.Sleep(1000);
        Player.speed = 4;
    }

    void Awake()
    {
        ItemDistributor();
    }
}

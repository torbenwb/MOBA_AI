using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Sprite minionSprite;
    public Sprite towerSprite;
    public Sprite heroSprite;
    public Sprite nexusSprite;
    public GameObject iconPrototype;
    public float mapScale = 0.1f;
    List<Image> icons = new List<Image>();
    int iconIndex = 0;

    void Start(){
        StartCoroutine(UpdateIcons());
    }

    Image GetIcon(int index)
    {
        if (index < icons.Count) {
            icons[index].gameObject.SetActive(true);
            return icons[index];
        }

        GameObject clone = Instantiate(iconPrototype);
        clone.transform.SetParent(transform);
        clone.gameObject.SetActive(true);
        icons.Add(clone.GetComponent<Image>());
        return clone.GetComponent<Image>();
    }

    void DrawUnitIcon(Unit unit, int index){
        Image icon = GetIcon(index);
        icon.sprite = GetSprite(unit.unitType);
        Vector3 p = unit.transform.position;
        Vector3 screenPoint;
        screenPoint.x = p.x;
        screenPoint.y = p.z;
        screenPoint.z = 0f;
        icon.transform.localPosition = screenPoint * mapScale;

        if (unit.gameObject.layer == LayerMask.NameToLayer("Team 1")) icon.color = Color.blue;
        else icon.color = Color.red;
    }

    Sprite GetSprite(UnitType unitType){
        switch(unitType){
            case UnitType.Minion: return minionSprite;
            case UnitType.Hero: return heroSprite;
            case UnitType.Tower: return towerSprite;
            case UnitType.Nexus: return nexusSprite;
        }
        return null;
    }

    IEnumerator UpdateIcons()
    {
        while(true)
        {
            Unit[] units = FindObjectsOfType<Unit>();

            int i = 0; int j = 0;

            while(i < units.Length)
            {
                DrawUnitIcon(units[i++], i);
                j++;
            }

            while(j < icons.Count){
                icons[j++].gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}

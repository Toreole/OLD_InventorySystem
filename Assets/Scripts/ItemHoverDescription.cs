using UnityEngine;
using UnityEngine.UI;

//The hover thing lol idk.
public class ItemHoverDescription : MonoBehaviour
{
    //Since this will only exist once per scene, just make a static instance lol
    public static ItemHoverDescription instance;

    public Image itemImage;
    public Text itemName;
    public Text itemDescription;
    public Vector3 cornerOfsset;
    private Vector3 halfHeightAndWidth;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void SetItem(ItemSO item)
    {
        itemImage.sprite = item.sprite;
        itemName.text = item.name;
        itemDescription.text = item.description;

        //Calculate optimal size here.
        var t = transform as RectTransform;
        halfHeightAndWidth = new Vector2( t.rect.width * 0.5f, -t.rect.height * 0.5f);
        print(halfHeightAndWidth.x + " -- " + halfHeightAndWidth.y);
        //set this as active.
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position = Input.mousePosition + (cornerOfsset + halfHeightAndWidth);
    }

}
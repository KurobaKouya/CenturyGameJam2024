using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddSymbols : MonoBehaviour
{
    [SerializeField] MapManager map;
    [SerializeField] Image image;
    [SerializeField] List<Button> buttons;
    [SerializeField] List<Sprite> symbolSprites;
    [SerializeField] float scaleMult = 1f;

    Button current;
    Sprite currentSprite;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SelectButton(buttons[index], symbolSprites[index]));
            

        }
    }

    void SelectButton(Button button, Sprite sprite)
    {
        if (current == button)
        {
            current = null;
            currentSprite = null;
            button.image.color = Color.white;
        }
        else
        {
            current = button;
            currentSprite = sprite;
            button.image.color = Color.gray;
        }
            
    }
    
    void InstantiateImage(Vector3 pos, Sprite sprite, float scaleMult = 1f)
    {
        Image img = Instantiate(image, pos, Quaternion.identity, transform);
        img.sprite = sprite;
        img.transform.localScale *= scaleMult;
    }

    // Update is called once per frame
    void Update()
    {
        if (!map.CheckWithinMap())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            
            if (current)
                InstantiateImage(new Vector2(map.GetMouseWorldPosition().x, map.GetMouseWorldPosition().y), currentSprite, scaleMult);


        }
    }
}

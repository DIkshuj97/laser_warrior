using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    
        [SerializeField] SpriteRenderer spriteRenderer; //will store sprite renderer
        [SerializeField] string colour="Orange";
        [SerializeField] GameObject Redbuttons;
        [SerializeField] GameObject Greenbuttons;
        [SerializeField] GameObject Bluebuttons;
        [SerializeField] GameObject Orangebuttons;


    public void change(Sprite differentSprite)
        {
            spriteRenderer.sprite = differentSprite; //sets sprite renderers sprite
        }

    public void GetColour(string colourName)
    {
        colour= colourName;
    }

    private void Update()
    {
        if (colour == "Red")
        {
            Redbuttons.SetActive(true);
            Bluebuttons.SetActive(false);
            Greenbuttons.SetActive(false);
            Orangebuttons.SetActive(false);
        }

        else if(colour=="Blue")
        {
            Redbuttons.SetActive(false);
            Bluebuttons.SetActive(true);
            Greenbuttons.SetActive(false);
            Orangebuttons.SetActive(false);
        }

        else if (colour == "Green")
        {
            Redbuttons.SetActive(false);
            Bluebuttons.SetActive(false);
            Greenbuttons.SetActive(true);
            Orangebuttons.SetActive(false);
        }

        else if (colour == "Orange")
        {
            Redbuttons.SetActive(false);
            Bluebuttons.SetActive(false);
            Greenbuttons.SetActive(false);
            Orangebuttons.SetActive(true);
        }
    }

}

using UnityEngine;

public static class MenuStruct {

    public struct Menu
    {
        public int ID;
        public Vector2 rectScale;
        public Vector2[] positions;
        public bool horizontalNav;
        public GameObject UI;
    }

    public struct settingsMenu
    {
        public int optionsNumber;
        public string[] optionsName;
    }
}



using UnityEngine;

public class UpgradeMenuPointer : MonoBehaviour
{
    Vector2[,] pos = new Vector2[4, 4];
    public int x=1, y=1;
    RectTransform rectTransform;
    PlayerInput controls;
    bool ok;
    public EndlessArenaManager endless;

    private void Awake()
    {
        ok = true;
        y = 0;
        x = 0;
        controls = new PlayerInput();
        controls.Menu.Navigate.performed += ctx => navigate(ctx.ReadValue<Vector2>());
    }

    void navigate(Vector2 dir)
    {
        ok = false;

        if (dir.x >= 0.5)
            x++;
        else if (dir.x <= -0.5)
            x--;

        if (x >= 3)
            x = 0;
        else if (x < 0)
            x = 2;


        if (dir.y >= 0.5)
            y--;
        else if (dir.y <= -0.5)
            y++;

        if (y >= 3)
            y = 0;
        else if (y < 0)
            y = 2;

        ok = true;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        /* Declar toate pozitiile posibile pe care le poate lua pointer-ul in meniu, sub forma unui
         * tablou bidimensional cu 3 linii si 3 coloane care contine elemente de tip Vector2 */
        pos[1, 1] = new Vector2(-227.5f, 54f);
        pos[1, 2] = new Vector2(0f, 54f);
        pos[1, 3] = new Vector2(227.5f, 54f);
        pos[2, 1] = new Vector2(-227.5f, 0f);
        pos[2, 2] = new Vector2(0f, 0f);
        pos[2, 3] = new Vector2(227.5f, 0f);
        pos[3, 1] = new Vector2(-227.5f, -54f);
        pos[3, 2] = new Vector2(0, -54f);
        pos[3, 3] = new Vector2(227.5f, -54f);

        if(GameMaster.gameMode==2)
        {
            controls.Menu.Select.performed += ctx =>
                unlockEndless();
        }

    }

    void Update()
    {
        if (ok)
            LerpPos(pos[y + 1, x + 1]);
    }

    void LerpPos(Vector2 pos)
    { rectTransform.localPosition = Vector2.Lerp(rectTransform.localPosition, pos, 12f * Time.deltaTime); }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void unlockEndless()
    {
        if (EndlessArenaManager.score >= endless.buy(y, x, false))
            EndlessArenaManager.score -= endless.buy(y, x, true);
    }

}

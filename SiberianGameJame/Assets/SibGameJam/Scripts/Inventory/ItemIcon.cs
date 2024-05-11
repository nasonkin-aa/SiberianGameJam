using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{

    [SerializeField] string[] ItemFormFactor;
    Vector2Int originPos;

    enum direction
    {
        north,
        south,
        west,
        east
    }

    private void Awake()
    {
        if (ItemFormFactor.Length == 0)
        {
            ItemFormFactor = new string[] { "1" };
        }

        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void SetOriginPosition(Vector2Int originPos)
    {
        this.originPos = originPos;
    }

    public Vector2Int GetOriginPosition()
    {
        return originPos;
    }

    public Vector2Int GetItemSizes()
    {
        return new Vector2Int(ItemFormFactor[0].Length, ItemFormFactor.Length);
    }

    public string[] GetItemFormFactor()
    {
        return ItemFormFactor;
    }

    public void MoveItemOnGrid(Vector2 pos)
    {
        //transform.position = new Vector2(x, y);
        transform.localPosition = pos;
    }

    public void MoveItem(Vector2 pos)
    {
        //transform.position = new Vector2(x, y);
        transform.position = pos;
    }

    public void SetParent(Transform transform)
    {
        this.transform.SetParent(transform);
    }

    /*void TransposeForm()
    {
        int rows = ItemFormFactor.Length;
        int cols = ItemFormFactor[0].Length;

        char[][] transposedChars = new char[cols][];

        if (cols == rows)
        {

            //transposedChars = (string[])ItemFormFactor.Clone();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0;  j < i; j++)
                {
                    char temp = ItemFormFactor[i][j];
                    transposedChars[i][j] = ItemFormFactor[j][i];
                    transposedChars[j][i] = temp;
                }
            }
        }
        else
        {
            for (int column = 0; column < cols; column++)
            {
                transposedChars = new char[rows]
                for (int row = 0; row < rows; row++)
                {

                }
            }
        }
    }*/

}

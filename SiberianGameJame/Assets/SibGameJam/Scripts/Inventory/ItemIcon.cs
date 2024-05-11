using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{

    RectTransform rectTransform;

    [SerializeField] string[] ItemFormFactorOrig;
    [SerializeField] string[] ItemFormFactorCurr;
    Vector2Int originPos;

    enum direction
    {
        north,
        south,
        west,
        east
    }

    [SerializeField] direction itemDirection = direction.north;


    private void Awake()
    {
        if (ItemFormFactorOrig.Length == 0)
        {
            ItemFormFactorOrig = new string[] { "1" };
        }


        rectTransform = GetComponent<RectTransform>();
        //ItemFormFactor = (string[])TransposeForm().Clone();

        ItemFormFactorCurr = (string[])ItemFormFactorOrig.Clone();
        RotateItem();

        transform.GetComponent<Image>().raycastTarget = false;
    }

    private void Start()
    {
        //Debug.Log(string.Join("\n", TransposeForm(ItemFormFactorOrig)));

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
        return new Vector2Int(ItemFormFactorCurr[0].Length, ItemFormFactorCurr.Length);
    }

    public string[] GetItemFormFactor()
    {
        return ItemFormFactorCurr;
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

    public void RotateDirection()
    {
        switch (itemDirection)
        {
            case direction.north:
                itemDirection = direction.east;
                break;
            case direction.east:
                itemDirection = direction.south;
                break;
            case direction.south:
                itemDirection = direction.west;
                break;
            case direction.west:
                itemDirection = direction.north;
                break;
        }

        RotateItem();
    }

    void RotateItem()
    {
        switch (itemDirection)
        {
            case direction.north:

                ItemFormFactorCurr = (string[])ItemFormFactorOrig.Clone();

                rectTransform.eulerAngles = new Vector3(0, 0, 0);
                rectTransform.pivot = new Vector2(0, 1);
                break;

            case direction.east:
                string[] transponadedArr = (string[])TransposeForm(ItemFormFactorOrig);

                for (int i = 0; i < transponadedArr.Length; i++)
                {
                    char[] charArr = transponadedArr[i].ToCharArray();
                    Array.Reverse(charArr);
                    transponadedArr[i] = new string(charArr);
                }

                ItemFormFactorCurr = (string[])transponadedArr.Clone();

                rectTransform.eulerAngles = new Vector3(0, 0, 270);
                rectTransform.pivot = new Vector2(0, 0);
                break;

            case direction.south:
                string[] upsideDownArr = (string[])ItemFormFactorOrig.Clone();
                Array.Reverse(upsideDownArr);

                for (int i = 0; i < upsideDownArr.Length; i++)
                {
                    char[] charArr = upsideDownArr[i].ToCharArray();
                    Array.Reverse(charArr);
                    upsideDownArr[i] = new string(charArr);
                }

                ItemFormFactorCurr = (string[])upsideDownArr.Clone();

                rectTransform.eulerAngles = new Vector3(0, 0, 180);
                rectTransform.pivot = new Vector2(1, 0);
                break;

            case direction.west:
                //ItemFormFactor = (string[])TransposeForm().Clone();
                string[] transponadedArr1 = (string[])TransposeForm(ItemFormFactorOrig);

                /*for (int i = 0; i < transponadedArr1.Length; i++)
                {
                    char[] charArr = transponadedArr1[i].ToCharArray();
                    Array.Reverse(charArr);
                    transponadedArr1[i] = new string(charArr);
                }*/

                Array.Reverse(transponadedArr1);

                ItemFormFactorCurr = (string[])transponadedArr1.Clone();
                //ItemFormFactorCurr = (string[])TransposeForm(ItemFormFactorCurr);

                rectTransform.eulerAngles = new Vector3(0, 0, 90);
                rectTransform.pivot = new Vector2(1, 1);
                break;

        }
    }

    string[] TransposeForm(string[] arr)
    {

        int rows = arr.Length;
        int cols = arr[0].Length;

        char[][] transposedChars = new char[cols][];
        string[] transposed = new string[cols];

        if (cols == rows)
        {

            
            for (int c = 0; c < rows; c++)
            {
                transposedChars[c] = arr[c].ToCharArray();
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    char temp = transposedChars[i][j];
                    transposedChars[i][j] = transposedChars[j][i];
                    transposedChars[j][i] = temp;
                }
            }
        }
        else
        {
            for (int column = 0; column < cols; column++)
            {
                transposedChars[column] = new char[rows];
                for (int row = 0; row < rows; row++)
                {
                    transposedChars[column][row] = arr[row][column];
                }
            }
        }

        for (int k = 0; k < cols; k++)
        {
            transposed[k] = new string(transposedChars[k]);
        }

        return (string[])transposed.Clone();

    }

}

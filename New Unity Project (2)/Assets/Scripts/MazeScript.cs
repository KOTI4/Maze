using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public GameObject[] mazeSprites;

    // Start is called before the first frame update
    void Start()
    {
        var newMaze = new Maze(4, mazeSprites);
        newMaze.Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class Tile
    {
        public int X_coordinate { get; set; }
        public int Y_coordinate { get; set; }

        public GameObject sprite { get; set; }

        private bool up;
        public bool Up
        {
            get { return up; }
            set
            {
                if (
                    (down == false && right == false) ||
                    (right == false && left == false) ||
                    (down == false && left == false)
                  ) up = true;
                else up = value;
            }
        }

        private bool down;
        public bool Down
        {
            get { return down; }
            set
            {
                if (
                   (up == false && right == false) ||
                   (right == false && left == false) ||
                   (up == false && left == false)
                 ) down = true;
                else down = value;
            }
        }

        private bool right;
        public bool Right
        {
            get { return right; }
            set
            {
                if (
                   (up == false && down == false) ||
                   (down == false && left == false) ||
                   (up == false && left == false)
                 ) right = true;
                else right = value;
            }
        }

        private bool left;
        public bool Left
        {
            get { return left; }
            set
            {
                if (
                  (up == false && down == false) ||
                  (down == false && right == false) ||
                  (up == false && right == false)
                ) left = true;
                else left = value;
            }
        }

        public Tile()
        {


            this.Up = true;
            this.Down = true;
            this.Right = true;
            this.Left = true;
        }

        public static Tile TileRand(int coordX, int coordY, GameObject[] sprites)
        {
            var rnd = new System.Random();
            return new Tile
            {
                X_coordinate = coordX,
                Y_coordinate = coordY,

                Up = rnd.RandBool(),
                Down = rnd.RandBool(),
                Right = rnd.RandBool(),
                Left = rnd.RandBool(),


                sprite = GetSprite(sprites)

            };
        }

        public static GameObject GetSprite(GameObject[] sprites)
        {
            return sprites[0];
        }
    }

    public class Maze
    {
        public int startX { get; set; }
        public int startY { get; set; }

        public Tile[][] tiles;

        public Maze(int length, GameObject[] sprites)
        {
            startX = 0;
            startY = 0;

            tiles = new Tile[length][];

            for (int i = 0; i < length; i++)
            {
                var tempArray = new Tile[length];

                for (int j = 0; j < length; j++)
                    tempArray[j] = Tile.TileRand(i, j, sprites);

                tiles[i] = tempArray;
            }
        }

        public void Show()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {

                    var tile = Instantiate(tiles[i][j].sprite);

                    tile.transform.position = new Vector2(
                        tiles[i][j].X_coordinate,
                        tiles[i][j].Y_coordinate
                        );
                }
            }
        }
    }
}



public static class RandomExtension
{
    public static bool RandBool(this System.Random rnd)
    {
        if (rnd.Next(2) == 0) return true;
        else return false;
    }
}

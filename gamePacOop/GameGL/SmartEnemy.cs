﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gamePacOop.GameGL
{
    class SmartEnemy : Enemy
    {

        int speed;
        public SmartEnemy(Form1 form, int lives, Berzerk berzerk, Image image, GameCell startCell) : base(form, lives, berzerk, GameObjectType.ENEMY, image)
        {
            this.CurrentCell = startCell;
            this.berzerk = berzerk;
            this.speed = 1;
        }

        public override GameCell move()
        {
            if (speed % 3 == 0)
            {
                manageDirections();
                GameCell currentCell = this.CurrentCell;
                GameCell nextCell = currentCell.nextCell(direction);
                GameObject previousObject = nextCell.CurrentGameObject;
                this.CurrentCell = nextCell;

                if (previousObject.GameObjectType == GameObjectType.PLAYER)
                {
                    berzerk.decreaseHealth();
                }
                if (currentCell != nextCell)
                {
                    currentCell.setGameObject(previousObject);
                  

                }

                speed = 1;
                return nextCell;

            }
            speed++;
            return this.CurrentCell;
        }

        public void manageDirections()
        {
            double[] distance = new double[4] { 10000, 10000, 10000, 10000 };
            if (this.CurrentCell.nextCell(GameDirection.Left).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[0] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Left));
            }
            if (this.CurrentCell.nextCell(GameDirection.Right).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[1] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Right));
            }
            if (this.CurrentCell.nextCell(GameDirection.Up).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[2] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Up));
            }
            if (this.CurrentCell.nextCell(GameDirection.Down).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[3] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Down));
            }
            if (distance[0] <= distance[1] && distance[0] <= distance[2] && distance[0] <= distance[3])
            {
                this.direction = GameDirection.Left;

            }
            if (distance[1] <= distance[0] && distance[1] <= distance[2] && distance[1] <= distance[3])
            {
                this.direction = GameDirection.Right;

            }
            if (distance[2] <= distance[0] && distance[2] <= distance[1] && distance[2] <= distance[3])
            {
                this.direction = GameDirection.Up;

            }
            if (distance[3] <= distance[0] && distance[3] <= distance[1] && distance[3] <= distance[2])
            {
                this.direction = GameDirection.Down;

            }





        }


        public double calculateDistance(GameCell nextcell)
        {
            return Math.Sqrt(Math.Pow((berzerk.CurrentCell.X - nextcell.X), 2) + Math.Pow((berzerk.CurrentCell.Y - nextcell.Y), 2));
        }

    }
}

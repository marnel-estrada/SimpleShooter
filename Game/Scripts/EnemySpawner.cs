using System;
using System.Collections.Generic;

using Godot;

using Common;

namespace Game {
    class EnemySpawner : Node2D {

        [Export]
        public float numEnemiesVelocity = 0.1f;

        [Export]
        public float generateEnemyInterval = 2;

        private PackedScene enemyResource;

        private float numEnemies = 1;

        private float polledTime;

        private const int MIN_X = -700;
        private const int MAX_X = 700;

        private Random random = new Random();

        private Vector2 spawnerPosition;

        public override void _Ready() {
            base._Ready();

            this.enemyResource = (PackedScene)ResourceLoader.Load("res://Game/Scenes/EnemyShip.tscn");
            Assertion.AssertNotNull(this.enemyResource);

            this.spawnerPosition = this.GlobalPosition;

            // Spawn a single enemy right away
            Spawn();
        }

        public override void _Process(float delta) {
            base._Process(delta);

            this.numEnemies += this.numEnemiesVelocity * delta;

            // Check if it's time to spawn
            this.polledTime += delta;
            while(polledTime >= this.generateEnemyInterval) {
                // Time to spawn
                int spawnCount = (int)this.numEnemies;
                for(int i = 0; i < spawnCount; ++i) {
                    Spawn();
                }

                polledTime -= this.generateEnemyInterval;
            }
        }

        private void Spawn() {
            float randomX = this.random.Next(MIN_X, MAX_X);
            Vector2 position = new Vector2(this.spawnerPosition.x + randomX, this.spawnerPosition.y);

            Node2D enemy = this.enemyResource.Instance() as Node2D;
            AddChild(enemy);
            enemy.GlobalPosition = position;
        }

    }
}

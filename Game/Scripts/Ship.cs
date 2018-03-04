using System;
using System.Collections.Generic;

using Godot;

using Common;

namespace Game {
    class Ship : Node2D {
        
        // This used to be exported but sometimes it doesn't show on editor
        private float moveSpeed = 700;

        // This used to be exported but sometimes it doesn't show on editor
        private float fireRate = 8; // This is lasers per second

        private float currentMoveSpeed;

        private PackedScene laserResource;
        
        private Node2D muzzle;

        private float fireInterval;
        private float polledTime;

        private Node2D ship;

        public override void _Ready() {
            base._Ready();

            Assertion.Assert(this.moveSpeed > 0);

            this.laserResource = (PackedScene)ResourceLoader.Load("res://Game/Scenes/Laser.tscn");
            Assertion.AssertNotNull(this.laserResource);

            this.muzzle = (Node2D)FindNode("Muzzle");
            Assertion.AssertNotNull(this.muzzle);

            Assertion.Assert(this.fireRate > 0);
            this.fireInterval = 1.0f / this.fireRate;

            this.ship = (Node2D)FindNode("Ship");
            Assertion.AssertNotNull(this.ship);
        }

        public override void _Process(float delta) {
            base._Process(delta);

            if(Input.IsActionPressed("ui_right")) {
                this.currentMoveSpeed = this.moveSpeed;
            } else if (Input.IsActionPressed("ui_left")) {
                this.currentMoveSpeed = -this.moveSpeed;
            } else {
                this.currentMoveSpeed = 0;
            }

            // Update position
            Vector2 position = this.ship.Position;
            position.x += this.currentMoveSpeed * delta;
            this.ship.SetPosition(position);

            // Check for fire
            if(Input.IsActionJustPressed("fire")) {
                Fire();
            }

            // Check when fire key is held
            if(Input.IsActionPressed("fire")) {
                UpdateFireHeld(delta);
            } else {
                this.polledTime = 0;
            }
        }

        private void UpdateFireHeld(float delta) {
            this.polledTime += delta;
            while(this.polledTime >= this.fireInterval) {
                // Time to fire
                Fire();
                this.polledTime -= this.fireInterval;
            }
        }

        private void Fire() {
            Node2D laser = (Node2D)this.laserResource.Instance();
            AddChild(laser);
            laser.GlobalPosition = this.muzzle.GlobalPosition;
        }

    }
}

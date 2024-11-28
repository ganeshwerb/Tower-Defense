# Tower Defense Game - README

## Overview

This **Tower Defense** game challenges players to defend their base from waves of enemies. Players must place towers and use gold efficiently to destroy enemies before they reach the base. The game features a straightforward gameplay loop with two distinct tower types and a simple resource system.

---

## Objective

The goal is to prevent enemies from reaching your base, which has a total of **1500 HP**. You need to destroy enemies before they reach the base. Each enemy has **100 HP** and deals **100 HP damage** to the base. The game ends when the base’s health is reduced to **0 HP**.

---

## Game Design

### Core Gameplay Loop

1. **Enemy Waves**: Enemies spawn periodically and move along a fixed path toward the player’s base. Each enemy has **100 HP** and deals **100 HP damage** if they reach the base.
2. **Tower Placement**: Players place one of two tower types (Marshal and Odin) on a grid. Each tower has different costs and damage output.
3. **Resource Management**: Killing an enemy earns **50 gold**. This gold is used to place more towers to handle future waves.
4. **Game Over**: The game ends when the base’s health reaches **0 HP** (i.e., when too many enemies reach the base).

---

## Towers

There are two types of towers available:

1. **Marshal Tower**
   - **Cost**: 90 gold
   - **Fire Rate**: 1 shot per second
   - **Damage**: 100 HP per shot
   - **Role**: The Marshal tower is designed to deal heavy damage with slower fire rates. It is effective against enemies with moderate health.

2. **Odin Tower**
   - **Cost**: 45 gold
   - **Fire Rate**: 3 shots per second
   - **Damage**: 30 HP per shot
   - **Role**: The Odin tower fires more rapidly but deals less damage per shot. It is ideal for clearing waves of enemies more efficiently over time.

### No Upgrade System
- **Towers cannot be upgraded**. Once placed, they remain in their initial form for the duration of the game. This requires players to strategically choose which tower to place based on their available gold and the situation.

---

## Enemy Properties

- **Health**: Each enemy has **100 HP**.
- **Damage**: Each enemy deals **100 HP damage** to the player’s base upon reaching it.
- **Reward**: Each enemy killed rewards the player with **50 gold**.

---

## Base Properties

- **Health**: The base has a total of **1500 HP**.
- **Game Over**: The game ends when the base's health is reduced to **0 HP**, which occurs if enough enemies reach and deal damage to the base.

---

## Gold Economy Design

- **Gold Earnings**: Killing each enemy rewards the player with **50 gold**.
- **Gold Spending**: Gold is used to place towers:
  - **Marshal Tower**: Costs **90 gold**.
  - **Odin Tower**: Costs **45 gold**.

---

## Object Pooling for Enemies

- **Object Pooling**: To optimize performance, object pooling has been implemented for enemy creation and management. This reduces the overhead of frequently instantiating and destroying enemies, especially as the game progresses and more enemies spawn.

---

## Game Flow

1. **Start the Game**: The game begins with an initial wave of enemies. Players start with enough gold to place one or two towers.
2. **Defend the Base**: As enemies approach, players must strategically place and use their towers to destroy enemies.
3. **Earn Gold**: Players earn **50 gold** for every enemy they destroy. This gold is used to place additional towers or replace towers that are no longer effective.
4. **Survive the Waves**: The game continues through multiple waves of enemies, progressively becoming more challenging.
5. **Game Over**: The game ends when the base's HP reaches **0**, either due to too many enemies or not enough effective defense.

---

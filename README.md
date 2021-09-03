# Battleships Game Simulator

## Table of contents
* [Overview](#overview)
* [Rules](#rules)
* [Prerequisites](#prerequisites)
* [Technologies](#technologies)
* [Setup](#setup)

## Overview
Based on the rules of Battleship board game (https://en.wikipedia.org/wiki/Battleship_(game)) randomly places ships on two boards and simulates the gameplay between 2 players.
![Game Photo](_images/gameplay.png?raw=true "Game Photo")

The implementation contains following features:
* Automatic placement of ships in random places after user hit Start Game. Both Vertical and Horizontal search algorithms are implemented 
* It is possible to choose what type of a game will be played. (Currently configurable only via backend BattleshipGameService.StartNewGame by providing GameVersion from available options)
* Simulation of a game

## Rules
* There are two players
* They attack each other in turns
* If player fires at empty spot - he looses the turn
* If player fires at ship - he continues the turn
* If player fired at ship and it has no more health (the ship), player is certain that around the ship there is no possibility of having other ships
* Ships can be placed horizontally and vertically
* Player who will sunk all oponnent's ships is a winner.

## Prerequisites
* Visual Studio 2019
* .NET Core 5.0

## Technologies
* C#
* .NET Core 5.0
* Blazor Server

## Setup
To run / build application we simply start application in Visual Studio. 
As for environment application to be run needs Windows or Linux as operating system (preferred Windows)
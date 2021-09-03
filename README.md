# Battleships Game Simulator

## Table of contents
* [Overview](#overview)
* [Prerequisites](#prerequisites)
* [Technologies](#technologies)
* [Setup](#setup)

## Overview
Based on the rules of Battleship board game (https://en.wikipedia.org/wiki/Battleship_(game)) randomly places ships on two boards and simulates the gameplay between 2 players.

The implementation contains following features:
* Automatic placement of ships in random places after user hit Start Game. Both Vertical and Horizontal search algorithms are implemented 
* It is possible to choose what type of a game will be played. (Currently configurable only via backend BattleshipGameService.StartNewGame by providing GameVersion from available options)
* TODO: Simulation of a game 1. Fire shot, 2. Check if ship or not 3. Check if sunk 4. Check for end game
* TODO: Displaying a winner
* TODO: Restart game option

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
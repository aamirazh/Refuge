## Executive Summary

### Title

Refuge

### Genre

Roguelike/Roguelite

### Version

1.0.0

### The Big Idea

Refuge is a game that puts you in the shoes of a refugee fleeing from their country. Set in a 2D, roguelike, view-from-above environment, you are trying to get through different kinds of levels, or dungeons, in order to finally reach a country where you can be safe and raise your kids in peace.

### Platforms

This will be a downloadable game for the desktop.

### License

Undecided.

### Play Mechanic

Like many roguelikes, Refuge will be a turn-based dungeon-style game. Essentially, every time you move a tile, or do an action, every other "enemy" or character in the map moves or takes an action as well. There will be many different types of characters to interact with, some being enemies, some being allies, and some maybe neutral.

### Technology

This will be developed using the Unity Engine.

### Target Audience

Casual gamers who want to enjoy a short game without a huge commitment. Also people who are interested in the social and political commentary.

### Key Features and Unique Selling Points

Fun, roguelike features, as well as an interesting social and political commentary.

### Marketing Summary

Undecided for now. If completed, offer for free download online probably.

## Game Design Document

### Product Overview

Refuge will be a roguelike where you, as a refugee, try to progress through dungeons in order to get to a safe country to seek refuge in.

### The Core Concept

As you try to progress through these "dungeons" or environments and stages, you are met with many challenges. You have to get through officers looking to arrest you, potentially wild animals or traps to fight or avoid, and maybe even some puzzles to bypass. There may be some stealth features as well.

### Player Character

A refugee, presumably from Syria, trying to flee their war-ridden homeland to a country where they could make an honest living.

### Narrative Description of Gameplay

As stated before, the game is centered around fleeing your country and finding a safe country to start living in and seek refuge in. The gameplay is basically progressing through dungeons and environments in order to read this dream.

### Story

The refugee will be a refugee in his mid 20s, who was working as a doctor in Syria, before Russia destroyed the hospital he worked at. He lost friends, family, and patients to that explosion and decided he had enough. So, leaving his home, he went on to flee his country to look for a better life.

As he progresses through the dungeons, he gets closer and closer to his dream of living a stable life and being able to treat patients. Yet, once he reaches this country he idolizes, a reactionary president passes legislation that bans everyone coming from his country, and a racist border officer labels him as a terrorist. He finds himself fleeing this new country, unsure where to go after.

### Interface

Dungeon-like game. 2D, with a camera positioning from above.

### Obstacles

* Other AI characters.
* * Police officers looking for you to catch you.
* * Wild animals that try to hurt you.
* * Neutral characters that can either help you or hurt you.
* Potentially some puzzles in the dungeon, like something that needs a combination or a collection of items.

### Interactions

The user can interact with other characters or obstacles in a variety of ways. Depending on the object being interacted with, the user can potentially choose how they want to interact.

* Violent interactions, where the user has to battle the character or obstacle.
* "Social" interactions, where the user can talk to a character and get information or items from them.
* No interaction, where the user ignores the character or object.
* Catch interaction, where if the user interacts and doesn't successfully evade this character or object, they get caught and lose the game.
* Move or join option: Move onto or with the selected tile, terrain, or object.

### Level Walkthrough

The user would walk through this dungeon, trying to find where the exit is. Every time they move a tile, or do an action on another object or character (i.e. use a "turn"), every other character and active object in the map will also have a turn. Eventually, either the character will be caught, defeated in battle, or they would find the exit and successfully get through the level.

### Intro Cinematic

Simple story-telling of the plot background, and how the refugee is a doctor whose hospital was destroyed. May be just text, or maybe text with some still pictures. Will not likely be a cinematic.

### Game Shell

The game will be exported from the Unity engine and developer environment into an application.

### Controller Configuration

This game will be designed for the desktop, but the user could potentially define their own controls. These will probably be the simple controls, though:

* Arrow keys or WASD:
* * Left Arrow/A: Move left on map.
* * Down Arrow/S: Move down on map.
* * Right Arrow/D: Move right on map.
* * Up Arrow/W: Move up on map.
* Mouse:
* * Right click: Right click on a tile, object, or character to see your options of how to interact (see "Interactions" section).
* * Left click: Choose an option or carry out the default option for the tile/object selected.

### Character Actions

The character can either move or interact with objects. Movement is simple (up, down, left, right), and interactions are covered in the "Interactions" section.

### Exploration

There will be no exploration in this game, as it is not open world. The extent of exploration will be exploration in the dungeon, looking for items, secret doors, or simply the exit to end the level.

### Direct Effects On Character

The character can be affected by a few things, in a few different ways:
* Tiles:
* * Damage the character (unlikely to be implemented).
* * Make the character move slower.
* * Heal the character (unlikely to be implemented).
* Other Characters and Active Objects:
* * Damage the character actively, like a wild animal.
* * Arrest the character and end the game (game over).
* * Talk to the character or give them information.
* * Heal the character.
* Items:
* * Heal the character.
* * Buff the character in health or strength.
* * Show the character where the exit is, or give them sight of the dungeon (unlikely to be implemented).
* * Make the character move faster, in relation to the other characters in the map.

### Levels

The game will be split into two major parts: Before getting to the new country, and after being rejected by it (if we have time to include this). Each part will likely have three levels each, but all levels will be the same difficulty, and all will be randomly generated. The only major changes (in difficulty and gameplay) will happen in the middle, when the plotline changes trajectory.

### Art

Undecided, we will be using placeholders for now for our art and characters.

### Cut-Scene and Story Synopsis

There will be a few parts plot-wise here:

* Introduction. This will be text or image+text going through the plot background. (no gameplay)
* Getting to the dream country. This will be the gameplay portion, where the user progresses through dungeons, environments, and levels, to get to the next part.
* Plot twist. This will be text or image+text going through the plot change, when the refugee is rejected and hunted by authorities. (no gameplay)
* Fleeing dream country. This will be another gameplay portion where the user tries to progress to the last part.
* Conclusion and end of plot. This will be text or image+text going through the plot conclusion.

### Sound

Undecided, there will be no sound effects for now. Sound may be added after a basic version of the game is completed.

### Development Summary

* Week 1 (February 20-26): Basic development, setting up Unity environment, creating mechanics.
* Week 2 (February 27-March 5): Development of level building, potentially adding plot "cutscenes", adding different kinds of characters and items, or objects in general.
* Post-Week 2 (March 5): Reflection about progress, and if we should go further with this game and give more time to it.

### Localization

As of now, there are no plans to localize. We are planning on only marketing this for beta testing to a few students at Duke University, but we have no plans to release explicitly. If the game is finished, it may be released for free download online. The app will start out only having an English option and will not be abstracted internationally.

### Conclusion

Overall, Refuge is a roguelike, 2D, view-from-above game where the user progresses through dungeons, looking to seek refuge from an unstable homeland and an oppressive host country. This game looks to have fun, but simple gameplay, while also delivering a social message of the struggles of refugees fleeing from war in this day and age (as well as in history).

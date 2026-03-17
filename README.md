# GDIM32-Final
## Check-In

### Group Devlog

We used gizmos to help code the raycast of the lion. The lion uses raycasting to detect the player when it enters its line of sight, so that it looks more realistic than having the lion charge at the player even though they approached it while its back was turned. Furthermore in our final version there will be more lions so this helps balance the difficulty. We used gizmos to fire a ray in the direction the lion is looking at. This helped us instantly see how far the lion can see, which made it easier to know if it needed adjusting, and we could also see if it was obstructed or not. We also had it change colour depending on whether it can see depending on the value returned by CanSeePlayer() which is the method we used to see if the player was being seen by the lion.

Verion control was also used to fix merging issues. We had one huge version control crisis where one of us was working on something and hadn't committed the changes, and during that time someone else had pushed something. In attempt to merge the two, the entire scene where our game was just disappeared from the projects folder. We identified the mistake (that we didn't pull the changes first and instead tried to force push, which ended badly) and then we proceeded to revert the changes in github desktop in order to restore the game. In the process we lost a small amount of changes, but luckily they were easily redoable. 


### Romarick Anderson
My purpose in this project was to handle the logic systems within the game as well as contribute to the door and player assets. For logic systems, I worked on the PlayerMovement class, creating the initial player movement methods, and built the state machine and animation system. The methods that I created were HandleMovement(), UpdateAnim(), and ChangeState(PlayerState newState). I also developed the AnimationController for the player that uses animations, as well as the model from Omabuarts on itch.io. I built a camera system that allows the player to move their camera around and walk in the direction they are facing. Furthermore, I created the door logic that only opens if the player is holding a key in their inventory. This code is used within the Door script and is also used on the Door GameObject. Finally, I created the DialogueBox gameobject and logic for the dialogue options for the bird, which will appear when the player clicks on the bird and offers a hint if the player hits the button on the dialogue box. 

I believe that the Proposal breakdown was useful for establishing a strong foundation while also providing flexibility in developing our game. For instance, one of the mechanics we set out to create within our game was the pigeon hint system that provides players with information about the keys within the game. This inspired me to create a simple dialogue system that, when the player clicks on the bird, generates a small dialogue box with text that appears to provide the player with a hint if they press the V button, and will exit if the player presses tab. Although the initial idea for the pigeon dialogue system was slightly altered to fit the system created, the core idea of the system was implemented using the outline. The outline created goals for the task and systems that were needed to create the game, while also having the capacity to change if problems arose during the development process.



### Evrin (Hajin) Lee

I handled everything about the lion, and I made the player meat throw mechanic. The lion has three states, idle, wandering, and pursuing. I used an FSM to handle this. the lion starts in an idle state, where it plays the idle animation and it stays stationary for a set amount of seconds. in the wandering state, I set its navmesh destination to a random point on the arena and it walks there and goes back to idle. this repeats until its interrupted by the player entering its line of sight at a close enough distance. this is checked by checkdistance() and canseeplayer(). checkdistance is the method that calls EnterNewState() (which switches the lions state to the lionstate enum passed as a parameter) if both canseeplayer() returns true, and the player is close enough. The lion will remain in the pursuing state until either of those conditions become false. 


The meat throw mechanic is basically where you press space and the player throws a meat object from their inventory. I had to add a method called CountItem() to the inventory class in order to be able to check if there are any meat objects to throw in the players inventory. The MeatThrow() method in the player class handles the physics of throwing the meat object, and update() changes a member variable that stores the number of meat in the inventory to match. update() checks if the space ey was pressed, and if the meatcount isnt 0, ThrowMeat() gets called. 

I also designed and made the arena.

The outline we made in week 7 is really useful when it comes to making tasks for the trello board, which we use often to remind ourselves what needs to be done and visually shows us how much more we have left, which helps us figure out the minimum we need to get done every week. We haven't made any changes to our final plan. Considering our first attempt at our GDD for this game was quite vague, meaning we really focused on getting the mechanics and systems as specific as possible for the final plan, right now it's serving as a good guideline while we work. 
### Team Member Name 3
Put your individual check-in Devlog here.



## Final Submission
### Group Devlog

One design pattern we used was the state machine. This is very prominent in the code for the lion npc as the entire game is quite dependent on its movement. There were many instances where the lion had to switch between states, some examples including starting idle, then going to wandering, going from wandering to pursuing once the player was in sight, reverting back to wandering when the player got away or hid in a bush, and going into pursuing mode with the meat piece as the target when a piece is thrown. If we didn't use a state machine, all of the requirements for starting each state, such as setting a navmesh destination, playing animations, setting boolean variables back to their original states etc, would have to be typed for every single instance there was a state switch, and with how many we have it would be very redundant. Although the lion was mostly handled by one person, because we used a state machine, on the rare occasion another member had to use the lion code, they could switch between states without having to learn all of the logic every time they needed to. 

### Evrin (Hajin) Lee

Since the begining of the game one major problem we had was that the lion was really buggy and I was struggling a lot with the navmesh (which, to be fair was a problem until the very end). Since the checkup I identified that while the lion was in the wandering state it was trying to find a new destination every frame, which is why it moved so stiffly, and I fixed this by pulling out the part where they set a new destination so that it's no longer called in update. I also had to add variables like _needsNewDestination and _currentWanderingDestination in order to make sure I'm not generating a new wandering destination before I've reached the one I've already set. This made the lion move smoother, but I noticed the lion was still getting stuck. So I tried turning the colliders of the purely decorative elements of the terrain so that it'd phase through them, but it still got stuck sometimes. So I just decided to write a method that continuously checks if it's in the same position for longer than 0.5 seconds while its in the wandering state (CheckStuck()) and had it unstick itself every time that occured. (RecoverFromStuck()). the code to find a new destination is similar to my original one in my wandering state, but I did some googling (I had to do a LOT of googling to figure out how to use the navmesh in detail ) and learned that I can have it in a for loop, meaning it'll do it multiple times until I do find a destination that fits the navmesh criteria, instead of getting one, it not working, then having to wait the 0.5 seconds again for it to be called. 

I also coded the lions reaction to the meat throw, and player errors, such as when they use the wrong key or type the wrong code. I used a lot of events for these. For the player errors, I simply put the lion into "pursuing" when the event occurs. the meat throw was a lot more complicated. The teammate that had made the inventory had coded it so that the exact game object picked up gets destroyed and only gets stored as data. So i had to instantiate a new one every time I pressed space, store its landing location, compare its distance to the lion, check if it's close enough, and have the lion go to it. I also had to check that the meat had landed somewhere the lions navmesh can actually access as well. Then I had to change its state to pursuing, so i can have the _lionTarget (which is what gets set as the navmesh destination in the Pursuing() method) set as the location of the meat. Then I had to stop the navmesh, so that if the player got close to the lion when it's eating the meat, it will stay focused on the meat. So i had to make a separate meatTimer, where the lion plays the Idle animation for a set duration before going back to its normal activity. 

I also coded for the game over game state and the life count UI. It's game over when you get caught by the lion 3 times. This was done with basic collision detection using OnCollisionEnter. I also made this with events so that my teammates can also easily code for when you lose lives (OnLoseLife) and game over (OnGameOver). When you get caught, the player respawns in the spawn point I made in the arena by simply placing an empty game object and setting the players position to the spawn point object's position. When your life count gets to 0, the game freezes and you'll see the text "game over" appear. 




### Romarick Anderson 
Individual devlog


### Team Member Name 3
Ransom Liu

Group Devlog: 
Model - The PlayerMovement class is responsible for storing the "state" related to players, including input, and exposing only changes to the state through events. PlayerMovement has no references to Pigeon, Lion, Door, or Bush. As for input, this is managed in Update() and ClickedSystem(): Key Presses (for example, C for pigeon call) and Ray Cast Clicks cause events to be raised such as OnPigeonCallRequested?.Invoke(), doorClickedOn?.Invoke(), and birdClickedOn?.Invoke(). When a collision occurs with the lion, OnLoseLife?.Invoke() is invoked (line 211). Therefore, the model owns the state and "something happened" signals; however, the model itself has no knowledge of how the UI or NPCs will respond to these signals.

View - Views are the components used to display or hide information in response to those signals. Pigeon is a View: In Start() (line 39), it subscribes to birdClickedOn and determines if the dialogue box should be active (dialougeBox.SetActive) and what the hintText should be. The game-over UI in the GameController (i.e. _gameOverUI.SetActive(true) in CheckGameOver()) is another View. Door modifies its own transform and text; BushHidingZone is the "view" of the bush as both an interactive zone (trigger volume) and as providing feedback to the model about the input it received from the zone (by calling SetHidden).

Controller - Controllers listen to the model's signals (events) and determine how they want to react to them and therefore drive Views or other behavior. PigeonCallController listens for Player.OnPigeonCallRequested (line 8) and in the event handler (lines 17-29), it simply determines whether to call either CurrentPigeon.Enable() or Disable() — it doesn't receive any raw input or have knowledge of any pigeon state. GameController listens for Player.OnLoseLife and causes the game-over UI and time scale to be enabled. Door listens for Player.doorClickedOn (line 26) and runs doorLogic(); it then raises correctKey, which is used by Pigeon to move to the next line of dialogue. Lion listens for Player.OnPlayerHidden and, when the player is hidden, it will switch to walking rather than chasing. Therefore, each Controller responds to one or more model signals (events) and alters either a View or the state of a NPC; the model remains completely unaware of the Controllers.


## Open-Source Assets
Cite any open-source assets here. Put them in a LIST, and use correctly formatted LINKS.

[Monkey Model Asset](https://omabuarts.itch.io/quirky-series-free-animals)

[Door Asset](https://www.fab.com/listings/41516fcd-3362-4a4f-8f98-c72c74c60dfa)

[Jungle Music](https://www.youtube.com/watch?v=QJWxzGdrhpY)

[Lion Asset- Model and animations](https://sketchfab.com/3d-models/animated-lion-3d-animal-model-1c3cd9595ae34eee92f957d67facc08d)

[Fence Asset](https://assetstore.unity.com/packages/3d/chainlink-fences-73107)

[Nature Asset Pack- used for terrain and its elements](https://assetstore.unity.com/packages/3d/environments/ultimate-nature-lite-176906)

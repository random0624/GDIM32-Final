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

### Team Member Name 1

### Team Member Name 2
Put your individual final Devlog here.
### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
Cite any open-source assets here. Put them in a LIST, and use correctly formatted LINKS.
[Monkey Model Asset](https://omabuarts.itch.io/quirky-series-free-animals)

[Door Asset](https://www.fab.com/listings/41516fcd-3362-4a4f-8f98-c72c74c60dfa)

[Jungle Music](https://www.youtube.com/watch?v=QJWxzGdrhpY)

[Lion Asset- Model and animations](https://sketchfab.com/3d-models/animated-lion-3d-animal-model-1c3cd9595ae34eee92f957d67facc08d)

[Fence Asset](https://assetstore.unity.com/packages/3d/chainlink-fences-73107)

[Nature Asset Pack- used for terrain and its elements](https://assetstore.unity.com/packages/3d/environments/ultimate-nature-lite-176906)

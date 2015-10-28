###Introduction
NecroDancer Score Analyzer is a trainer for a video game named Crypt of the NecroDancer. It counts the amount of gold/score by different sources (gold from bosses, gold from non-boss monsters, gold from golden shop walls, penalty by crown of greed, etc.). This trainer helps me understand my score better.

###How to use
Run the analyzer side by side with the game. 

It shows the amount of gold from different sources while you are playing all-zones mode or watching an all-zones mode replay (either local replay or from the leaderboard). If you are playing other modes, it shows some meaningless number.

###Accuracy
The numbers are not 100% accurate, however they should be good estimates, especially for decent score runs. They might be significantly inaccurate under some certain circumstances, e.g.

1. In a run you have touched the Crown of Greed but later you abandoned it.
2. In a level you distroyed many golden walls but you didn't pick up the gold.
3. You killed the boss in a boss level but you didn't pick up the rewarding gold.
4. Maybe some other rare circumstances.

Why the numbers are not accurate? The main reason is that, most of the gold goes through two steps to end up into your score counter: firstly it spawns and lies on the ground, and secondly it is collected by you when you step onto it. The analyzer does not know where a pile of gold comes from when you are collecting it, so in order to analyze the sources of gold it count gold when the gold spawns. And when doing in this way, the analyzer does not know whether you will collect all the gold that have spawned, so it basicly assumes that you will collect all of them. 

###How it is made
1. A cheat table used in Cheat Engine that do the exactly same thing is made.
2. While the cheat table is set in effect, I use memreader to read the modified game memory and save the data to two .bin files.
3. The main program reads the two .bin files and writes the data into game memory, so that Cheat Engine is not needed any more.
All the related codes (the cheat table, memreader and the main program) are included in the source code if anyone would want to check.

gjchangmu
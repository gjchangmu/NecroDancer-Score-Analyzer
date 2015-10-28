#Introduction
NecroDancer Score Analyzer is a trainer for a video game named Crypt of the NecroDancer. It counts the amount of gold/score by different sources (Boss drop, Non-boss monsters drop, Golden walls drop, Crown of Greed penalty, etc.).

#How to use
Run the analyzer side by side with the game. 
It shows the amount of gold from different sources while you are playing all-zones mode or watching an all-zones mode replay (either local replay or from the leaderboard). If you are playing other modes, it shows some meaningless number.

#Accuracy
The numbers are not 100% accurate, however they should be good estimates. They might be significantly inaccurate under some certain circumstances:
1. In a run you have touched the Crown of Greed but later you abandoned it.
2. In a level you distroyed many golden walls but you didn't pick up the gold.
3. You killed the boss in a boss level but you didn't pick up the rewarding gold.
4. Maybe some other rare circumstances.

#How it is made
1. A cheat table used in Cheat Engine that do the exactly same thing is made.
2. While the cheat table is set in effect, I use memreader to read the modified game memory and save the data to two .bin files.
3. The main program reads the two .bin files and writes the data into game memory, so that Cheat Engine is not needed any more.
All the related codes (the cheat table, memreader and the main program) are included in the source code if anyone would want to check.

gjchangmu
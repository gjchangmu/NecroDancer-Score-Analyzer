###Introduction
NecroDancer Score Analyzer is a trainer for a video game named Crypt of the NecroDancer. It counts the amount of gold/score by different sources (gold from bosses, gold from non-boss monsters, gold from golden shop walls, penalty by crown of greed, etc.). This trainer helps me understand my score better.

This is in beta status and any kind of suggestions are welcome.

###How to use
Run the analyzer side by side with the game. 

It shows the amount of gold from different sources while you are playing all-zones mode or watching an all-zones mode replay (either local replay or from the leaderboard). If you are playing other modes, it shows some meaningless number.

###Accuracy
The numbers are not 100% accurate, however they should be good estimates, especially for decent score runs. They might be significantly inaccurate under some certain circumstances, e.g.

1. In a run you have touched the Crown of Greed but later you abandoned it.
2. In a level you destroyed many golden walls but you didn't pick up the gold.
3. You killed the boss in a boss level but you didn't pick up the rewarding gold.
4. Maybe some other rare circumstances.

Why the numbers are not accurate? The main reason is that, most of the gold goes through two steps to end up into your score counter: firstly it spawns and lies on the ground, and secondly it is collected by you when you step onto it. The analyzer does not know where a pile of gold comes from when you are collecting it, so in order to analyze the sources of gold it count gold when the gold spawns. And when doing in this way, the analyzer does not know whether you will collect all the gold that have spawned, so it basically assumes that you will collect all of them. 

###Is it a cheat?
No, the analyzer does not do any kind of cheat. I believe that your runs should be totally legit while the analyzer is running, as the only thing the analyzer does is counting your gold. However as the analyzer do modify the game memory, I cann't say that I'm 100% sure the analyzer cause no side-effect to the game, as there might be bugs in the analyzer.

One thing that concerns me now is that, the analyzer show the amount of gold in hidden rooms in current level right after you enter the level, assuming that you will collect them. So if you look at the analyzer right after you enter a new level, you will know whether there is gold in hidden rooms in the level. By now I haven't figured out a way to remove this other than hiding the information. Luckily this is a very trivial thing, so trivial that I believe players won't ever benefit from it, so I didn't hide it.

###How it is made
1. I made a cheat table used in Cheat Engine that do the exactly same thing.
2. I used memreader to track which parts of the game code were modified by the cheat table and I stored the difference into diff.bin. 
3. I used memreader to read out the injected code in the newly allocated memory region and saved it into storage.bin.
4. The main program reads the two .bin files and writes the data into game memory with some manual address rebase, so that Cheat Engine is not needed any more.
All the related codes (the cheat table, memreader and the main program) are included in the source code if anyone would want to check.

gjchangmu (gujianjiayi4@126.com)
EXTERNAL VerifyQuest()
VAR questCompleted = false

-> start

=== start ===
A long time ago, a couple of orcs stole my candy.
It would be nice if I could get that sweet ass candy back.
By chance, have you come across my candy, bitch?
    * [Don't even trip dog!]
        ~VerifyQuest()
        { questCompleted:
            -> success
        - else:
            -> lie
        }
    * [Not yet.]
        -> noCandy
-> END


=== success ===
AAAAWE,
Bitch!
Here's a reward.
-> END

=== noCandy ===
Looks like you don't have my candy bitch.
-> END

=== lie ===
If you continue to tell filfthy lies you might wake up with a pumpkin for a head...
-> END

=== postCompletion ===
Thanks for helping me find my candy bitch.
-> END
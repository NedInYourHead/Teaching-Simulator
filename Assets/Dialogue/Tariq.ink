VAR myBehaviour = -> normal
VAR isBehind = true
VAR questionSlide = "A"
VAR studentName = "Tariq"

-> myBehaviour

//{~-> normal|-> sleeping|-> talking|-> handUp}

=== class_not_started ===
{~sir, you haven't even started the slides yet!|you need to go to the front and teach us if you want to us to learn anything...}
->END


=== normal ===
{~*Is frantically taking notes*|Hello sir!|Yes?}
    * Did you see that ludicrous display last night?
       What was Wenger thinking, sending Walcott on that early?
       * * [\* go back to teaching*] you go back to teaching
        ->DONE
    
    * Do you need help with anything?
        {isBehind:
            {~Yes|Yeah|Yep}... <>
                -> handUp
        - else:
            {~I think I'm alright for now sir|Not right now, thanks for asking}
        }
        * *[\* go back to teaching*] you go back to teaching

->END


=== sleeping ===
{~*is lying head down on the desk, eyes closed*|*is staring into space before looking up at you, startled* hello sir!|*is looking out the window with a bored expression*}
    * Are you paying full attention, {studentName}?
    {~Sorry sir...|*blinks a few times* *mumbles an apology*}
    * * [\* go back to teaching*] you go back to teaching

->END


=== talking ===
{~*is leaning closer to their classmate to hear what they're whispering*|Erm... we were talking about the lesson sir!|*quietly gossiping to friend* "did you hear about what happened last weekend?"}
    * Are you paying full attention, {studentName}?
    {~Okay sir...|I wasn't talking...|I just wanted to say-}
        * * You shouldn't disrupt the learning of others.
            * * * [\* go back to teaching*] *you go back to teaching*

->END


=== handUp ===
{~Could you clarify what you meant by {questionSlide}?|I don't quite understand {questionSlide}, could you explain it again?|Sorry, I didn't get all of {questionSlide} down, could you go back?}
    * hold {questionSlide} #showBar
    ->DONE
    * sorry, we haven't got enough time.
        * * [\* go back to teaching*] you go back to teaching
->END
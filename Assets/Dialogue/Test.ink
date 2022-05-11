VAR myBehaviour = -> normal
VAR isBehind = true
VAR questionSlide = "A"
VAR studentName = "Student 10"

{~-> normal|-> sleeping|-> talking|-> handUp}

-> myBehaviour

=== class_not_started ===
{~sir, you haven't even started the slides yet!|you need to go to the front and teach us if you want to us to learn anything...}
->END


=== normal ===
{~*Is frantically taking notes*|Hello sir!|Yes?}
    * How are you doing?
        //start status update
        -> DONE
    * Do you need help with anything?
    {isBehind:
    {~Yes|Yeah}, <>
        -> handUp
    - else:
        {~I think I'm alright for now sir|Not right now, thanks for asking}
    }
    ->DONE

->END


=== sleeping ===
{~*is lying head down on the desk, eyes closed*|*is staring into space before looking up at you, startled* hello sir!|*is looking out the window with a bored expression*}
    * Are you paying full attention, {studentName}?
    {~Sorry sir...|*blinks a few times* *mumbles an apology*}
    ->DONE

->END


=== talking ===
{~*is leaning closer to their classmate to hear what they're whispering*|Erm... we were talking about the lesson sir!|*quietly gossiping to friend* "did you hear about what happened last weekend?"}
    * Are you paying full attention, {studentName}?
    {~Okay sir...|I wasn't talking...|I just wanted to say-}
        * * Well you shouldn't disrupt the learning of others.
        -> DONE

->END


=== handUp ===
{~Could you clarify what you meant by {questionSlide}?|I don't quite understand {questionSlide}, could you explain it again?|Sorry, I didn't get all of {questionSlide} down, could you go back?}
    * hold {questionSlide} #show bar
    ->DONE
    * sorry, we haven't got enough time.
    //Start RunOutOfPatience
    ->DONE
->END
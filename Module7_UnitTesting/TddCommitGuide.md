# Commit History and TDD Flow

The commit history should show the work in small TDD steps. That means each change is built around a failing test, the smallest code change to make it pass, and then a refactor if needed.

A good flow is:
1. write a failing test first
2. commit the test
3. add the minimal code to pass
4. commit the fix
5. refactor if needed
6. commit the refactor separately if it changes the code
7. repeat for the next small behavior

The goal is that the git history tells the story of how the solution was built, not just the final result.

# Good Commit Message Rules

A good commit message should:
1. use imperative mood, like "Add spare scoring"
2. be short and specific
3. avoid unrelated changes in one commit
4. explain why when the reason is not obvious
5. keep the subject line readable
6. use a separate body if more detail is needed
7. match the actual change in the commit

# Sample Commit Sequence for One Kata

Example kata: Bowling Game.

1. `Add gutter game test`
2. `Implement basic score accumulation`
3. `Add spare scoring test`
4. `Handle spare bonus in score`
5. `Add strike scoring test`
6. `Handle strike bonus in score`
7. `Refactor bowling score calculation`
8. `Add perfect game test and pass it`

This sequence shows a proper TDD story because the history moves from red test, to minimal fix, to refactor, then repeats for the next behavior.

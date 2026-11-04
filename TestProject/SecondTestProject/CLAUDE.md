# Project Instructions

## Code review
After finishing any code change (new feature, bug fix, refactor), run the `code-reviewer` subagent on the diff (`git diff`) before reporting the task as done. Include its findings and verdict (APPROVE / REQUEST CHANGES / COMMENT) in your final summary, and fix any CRITICAL or HIGH issues it reports at high confidence. Skip the review for trivial non-code edits (docs, comments, formatting).

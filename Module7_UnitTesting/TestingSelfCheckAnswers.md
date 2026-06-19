# Testing Self-Check Answers

## 1. What is a unit test? What other types of tests exist?

A unit test checks a small piece of code in isolation, usually a function, method, or class behavior. It should be fast, deterministic, and focused on one thing.

Other common test types include:
- Integration tests: verify that multiple components work together.
- End-to-end tests: verify the full system from the user perspective.
- System tests: validate the application as a whole.
- Acceptance tests: check whether the software meets business requirements.
- Smoke tests: quick checks that the most important features still work.
- Regression tests: confirm that previously working behavior still works after a change.

## 2. What is the testing pyramid?

The testing pyramid is a strategy for balancing test types.
- Many unit tests at the bottom.
- Fewer integration tests in the middle.
- Very few end-to-end tests at the top.

The idea is that unit tests are cheaper, faster, and easier to maintain, while end-to-end tests are slower and more fragile.

## 3. What is TDD and BDD?

TDD means Test-Driven Development. You write a failing test first, then write the smallest amount of code to make it pass, then refactor.

BDD means Behavior-Driven Development. It focuses on behavior from the user's or business point of view and often uses readable scenarios like Given-When-Then.

In short:
- TDD is about guiding design through tests.
- BDD is about describing behavior in business-friendly language.

## 4. What is a mock, stub and fake?

These are test doubles, used to replace real dependencies in tests.

- Stub: returns predefined data and is used to control test input.
- Mock: verifies that certain interactions or method calls happened.
- Fake: a simplified working implementation, such as an in-memory repository.

A simple rule of thumb:
- Use a stub when you need data.
- Use a mock when you need to verify behavior.
- Use a fake when you want a lightweight real implementation.

## 5. Which test frameworks in C# do you know?

Common C# test frameworks include:
- MSTest
- NUnit
- xUnit
- xUnit.v3
- FluentAssertions is not a framework, but it is often used together with these frameworks for clearer assertions.

## 6. What best practices of unit testing do you know? What is AAA, FIRST?

Good unit testing practices:
- Test one behavior at a time.
- Keep tests small and readable.
- Use descriptive test names.
- Avoid testing implementation details.
- Make tests independent from each other.
- Keep tests fast and deterministic.
- Prefer simple assertions and clear setup.
- Test edge cases and error cases as well as the happy path.
- Refactor test code when needed.

AAA means:
- Arrange: set up the test data and dependencies.
- Act: call the method under test.
- Assert: verify the result.

FIRST is a set of unit test quality principles:
- Fast: tests should run quickly.
- Independent: tests should not depend on each other.
- Repeatable: tests should produce the same result every time.
- Self-validating: tests should clearly pass or fail.
- Timely: tests should be written at the right time, usually before or alongside production code.

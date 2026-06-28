# Module 7 Katas: TDD Grouping

This is a practical way to group the katas by idea, so you can choose a set that tells a clear TDD story.

## 1. Pure Rules

These are the easiest to explain and the simplest to grow with red-green-refactor.

1. [OddEven_20](list/OddEven_20/README.md) - simplest rule-based classification.
2. [LeapYear_17](list/LeapYear_17/README.md) - a few clear branching rules.
3. [FizzBuzz_12](list/FizzBuzz_12/README.md) - simple branching and sequence rules.
4. [PrimeFactor_22](list/PrimeFactor_22/README.md) - classification with a bit more logic.

## 2. Small Algorithms

These still stay compact, but introduce counting, comparison, or small stateful calculations.

1. [BinaryGap_5](list/BinaryGap_5/README.md) - small algorithm with straightforward cases.
2. [CalcStats_7](list/CalcStats_7/README.md) - aggregation over a small input set.
3. [Diversion_10](list/Diversion_10/README.md) - compact algorithmic counting.
4. [Equi_11](list/Equi_11/README.md) - careful index reasoning.
5. [Circularprimes_8](list/Circularprimes_8/README.md) - more algorithmic, but still testable in layers.

## 3. String Parsing And Formatting

These are good when you want TDD practice around transformation, parsing, or output shape.

1. [StringSum_27](list/StringSum_27/README.md) - basic parsing plus addition.
2. [DictionaryReplacer_9](list/DictionaryReplacer_9/README.md) - lookup-based string replacement.
3. [StringCalculator_26](list/StringCalculator_26/README.md) - parsing grows gradually, which is good TDD practice.
4. [NaturalStringSorting_19](list/NaturalStringSorting_19/README.md) - string comparison gets trickier.
5. [BankOCR_2](list/BankOCR_2/README.md) - parsing structured text is more involved.
6. [BerlinClock_4](list/BerlinClock_4/README.md) - mapping time to a formatted representation.
7. [LCD-Digits_16](list/LCD-Digits_16/README.md) - display formatting with multiple digits.
8. [WordWrap_31](list/WordWrap_31/README.md) - deceptively simple, but wrapping rules get subtle.

## 4. Mutable State And Grids

These add state progression, neighborhood checks, or directional rules.

1. [RecentlyUsedList_23](list/RecentlyUsedList_23/README.md) - mutable state and eviction rules.
2. [MineFields_18](list/MineFields_18/README.md) - neighborhood calculation adds complexity.
3. [ReversePolishNotation_24](list/ReversePolishNotation_24/README.md) - stack evaluation, but still contained.
4. [GameOfLife_13](list/GameOfLife_13/README.md) - grid evolution with neighbor rules.
5. [Reversi_25](list/Reversi_25/README.md) - board logic and directional scanning.
6. [Labyrinth_15](list/Labyrinth_15/README.md) - pathfinding and grid traversal.

## 5. Classic Rule-Heavy Katas

These are the ones that usually take more careful commit structure and more edge cases.

1. [BowlingGame_6](list/BowlingGame_6/README.md) - classic TDD kata, but bonus scoring adds complexity.
2. [TennisGame_30](list/TennisGame_30/README.md) - scoring states and special naming rules.
3. [HarryPotter_14](list/HarryPotter_14/README.md) - discount combinations and optimization.
4. [PockerHands_21](list/PockerHands_21/README.md) - hand ranking comparison has lots of edge cases.
5. [Sudoku_28](list/Sudoku_28/README.md) - validation rules span rows, columns, and boxes.
6. [BearAndSteadyGene_3](list/BearAndSteadyGene_3/README.md) - sliding-window style logic takes more care.
7. [AStar_1](list/AStar_1/README.md) - search/pathfinding is harder to grow cleanly with TDD.

## 6. Design-Only

This one is not a normal implementation kata.

1. [SupermarketPricing_29](list/SupermarketPricing_29/README.md) - design-only, so it is not a normal TDD implementation kata.

## Notes

- This grouping is subjective and optimized for choosing a good TDD practice set, not for kata popularity.
- If you want the easiest three for a clean commit history, start from the Pure Rules or Small Algorithms groups.
- [SupermarketPricing_29](list/SupermarketPricing_29/README.md) stays separate because it is design-only rather than a normal coding kata.

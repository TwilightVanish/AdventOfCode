# 2024 Benchmarks

- 13th Gen Intel Core i9-13900K, 1 CPU, 32 logical and 24 physical cores
- Corsair Dual channel DDR5 5200 MHz 32GB 
---
### Day 01
| Method | Mean         | Error      | StdDev    | Gen0   | Allocated |
|------- |-------------:|-----------:|----------:|-------:|----------:|
| Part1  |     76.10 ns |   0.065 ns |  0.051 ns | 0.0020 |      40 B |
| Part2  |  1,130.67 ns |   1.912 ns |  1.695 ns | 0.0019 |      40 B |
| Parse  | 23,376.54 ns | 104.077 ns | 97.354 ns | 0.4272 |    8048 B |
---
### Day 02
| Method | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------- |----------:|----------:|----------:|-------:|-------:|----------:|
| Part1  |  3.022 us | 0.0458 us | 0.0406 us |      - |      - |      32 B |
| Part2  |  6.458 us | 0.0340 us | 0.0284 us |      - |      - |      32 B |
| Parse  | 18.813 us | 0.0576 us | 0.0481 us | 3.1738 | 0.4578 |   60256 B |
---
### Day 03
| Method | Mean       | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|------- |-----------:|---------:|---------:|-------:|-------:|----------:|
| Part1  |   216.1 ns |  0.95 ns |  0.89 ns | 0.0019 |      - |      40 B |
| Part2  |   438.7 ns |  5.57 ns |  5.21 ns | 0.0019 |      - |      40 B |
| Parse  | 7,404.0 ns | 79.77 ns | 74.61 ns | 0.8774 | 0.0229 |   16600 B |
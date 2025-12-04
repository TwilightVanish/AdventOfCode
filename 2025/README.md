# 2025 Benchmarks

- 13th Gen Intel Core i9-13900K, 1 CPU, 32 logical and 24 physical cores
- Corsair Dual channel DDR5 5200 MHz 32GB 
---
### Day 01
| Method | Mean     | Error     | StdDev    | Gen0   | Allocated |
|------- |---------:|----------:|----------:|-------:|----------:|
| Part1  | 4.267 us | 0.0101 us | 0.0094 us |      - |      32 B |
| Part2  | 5.643 us | 0.0157 us | 0.0147 us |      - |      32 B |
| Parse  | 6.835 us | 0.0695 us | 0.0650 us | 0.9384 |   17752 B |
---
### Day 02
| Method | Mean         | Error      | StdDev     | Gen0      | Gen1     | Gen2     | Allocated  |
|------- |-------------:|-----------:|-----------:|----------:|---------:|---------:|-----------:|
| Part1  |   538.949 us |  3.3755 us |  2.8187 us |  339.8438 | 333.0078 | 333.0078 |  2049.1 KB |
| Part2  | 2,274.377 us | 35.1872 us | 31.1925 us | 1000.0000 | 988.2813 | 988.2813 | 6688.13 KB |
| Parse  |     1.359 us |  0.0051 us |  0.0045 us |    0.3166 |   0.0019 |        - |    5.84 KB |
---
### Day 03
| Method | Mean     | Error    | StdDev   | Allocated |
|------- |---------:|---------:|---------:|----------:|
| Part1  | 14.18 us | 0.117 us | 0.110 us |      56 B |
| Part2  | 30.08 us | 0.038 us | 0.035 us |      80 B |
---
### Day 04
| Method | Mean        | Error     | StdDev    | Gen0     | Gen1    | Allocated  |
|------- |------------:|----------:|----------:|---------:|--------:|-----------:|
| Part1  |    40.97 us |  0.419 us |  0.372 us |   1.5869 |  0.1221 |    29.8 KB |
| Part2  | 2,221.20 us | 41.999 us | 56.067 us | 179.6875 | 19.5313 | 3311.38 KB |
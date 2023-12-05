# 2023 Benchmarks

- 13th Gen Intel Core i9-13900K, 1 CPU, 32 logical and 24 physical cores
- Corsair Dual channel DDR5 5200 MHz 32GB 
---
### Day 01
| Method | Mean      | Error     | StdDev    | Gen0    | Allocated |
|------- |----------:|----------:|----------:|--------:|----------:|
| Part1  |  8.295 us | 0.0906 us | 0.0847 us |       - |      32 B |
| Part2  | 57.123 us | 0.4452 us | 0.4165 us | 20.8130 |  392536 B |
---
### Day 02
| Method | Mean       | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|------- |-----------:|---------:|---------:|-------:|-------:|----------:|
| Part1  |   705.7 ns |  2.94 ns |  2.75 ns | 0.0010 |      - |      32 B |
| Part2  | 1,279.9 ns |  4.76 ns |  4.45 ns |      - |      - |      32 B |
| Parse  | 4,195.6 ns | 18.15 ns | 16.09 ns | 0.6714 | 0.0229 |   12736 B |
---
### Day 03
| Method | Mean      | Error     | StdDev    | Gen0    | Gen1   | Allocated |
|------- |----------:|----------:|----------:|--------:|-------:|----------:|
| Part1  | 54.153 us | 0.4052 us | 0.3790 us |  8.1787 |      - | 150.93 KB |
| Part2  | 59.175 us | 0.5184 us | 0.4849 us | 11.6577 |      - | 215.03 KB |
| Parse  |  2.282 us | 0.0239 us | 0.0200 us |  2.3232 | 0.2861 |  42.73 KB |
---
### Day 04
| Method | Mean     | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------- |---------:|----------:|----------:|-------:|-------:|----------:|
| Part1  | 8.107 us | 0.0904 us | 0.0802 us |      - |      - |      32 B |
| Part2  | 7.777 us | 0.1107 us | 0.0981 us |      - |      - |      46 B |
| Parse  | 9.638 us | 0.1039 us | 0.0972 us | 2.3193 | 0.2899 |   43656 B |
---
### Day 05
| Method | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------- |----------:|----------:|----------:|-------:|-------:|----------:|
| Part1  |  3.516 us | 0.0341 us | 0.0319 us |      - |      - |      40 B |
| Part2  | 12.951 us | 0.1128 us | 0.1055 us | 0.9766 | 0.0153 |   18512 B |
| Parse  | 21.345 us | 0.1849 us | 0.1729 us | 4.5166 | 0.1221 |   85312 B |